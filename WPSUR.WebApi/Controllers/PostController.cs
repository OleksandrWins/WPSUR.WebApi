using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WPSUR.Services.Interfaces;
using WPSUR.Services.Models.Post;
using WPSUR.WebApi.Models.Post;
using WPSUR.WebApi.Models.Tags;
using WPSUR.Services.Exceptions.PostExceptions;
using WPSUR.WebApi.Models.Tags;
using WPSUR.Services.Exceptions.PostExceptions;
using Azure.Core;

namespace WPSUR.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ApiControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService ?? throw new ArgumentNullException(nameof(postService));
        }

        [Authorize]
        [HttpPost("CreatePost")]
        public async Task<IActionResult> CreatePost([FromBody] PostRequest postData)
        {
            var postModel = new PostModel()
            {
                Title = postData.Title,
                Body = postData.Body,
                MainTag = postData.MainTag,
                SubTags = postData.SubTags,
                UserId = LoggedInUserId,
            };
            try
            {
                await _postService.CreatePostAsync(postModel);
                return Ok();
            }
            catch (LengthOfBodyException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (LengthOfTitleException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (NullReferenceException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (Exception)
            {
                return BadRequest("An unknown error occurred. Try again.");
            }
        }

        [Authorize]
        [HttpGet("SearchPostsByTags")]
        public async Task<ActionResult<ICollection<PostResponse>>> SearchPostsByMainTag([FromBody] SearchByMainAndSubTagsRequest request)
        {
            try
            {
                ICollection<PostModel> postModels = await _postService.ReceivePostsByMainTagAndSubTagAsync(request.MainTagId, request.SubTagId);
                ICollection<PostResponse> postResponses = new List<PostResponse>();
                foreach (PostModel postModel in postModels)
                {
                    PostResponse postResponse = new()
                    {
                        Title = postModel.Title,
                        Body = postModel.Body,
                    };
                    postResponses.Add(postResponse);
                }
                return Ok(postResponses);
            }
            catch (NullReferenceException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (Exception)
            {
                return BadRequest("An unknown error occurred. Try again.");
            }
        }

        [Authorize]
        [HttpGet("SearchPostsByField")]
        public async Task<ActionResult<ICollection<PostResponse>>> SearchPostsBySearchField([FromBody] PostTitleRequest request)
        {
            try
            {
                ICollection<PostModel> postModels = await _postService.ReceivePostsByTitleAsync(request.Title);
                ICollection<PostResponse> postResponses = new List<PostResponse>();
                foreach (PostModel postModel in postModels)
                {
                    PostResponse postResponse = new()
                    {
                        Title = postModel.Title,
                        Body = postModel.Body,
                    };
                    postResponses.Add(postResponse);
                }
                return Ok(postResponses);
            }
            catch (NullReferenceException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (Exception)
            {
                return BadRequest("An unknown error occurred. Try again.");
            }
        }
    }
}
