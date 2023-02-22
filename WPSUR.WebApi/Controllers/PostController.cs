using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WPSUR.Services.Interfaces;
using WPSUR.Services.Models.Post;
using WPSUR.Services.Models.Tags;
using WPSUR.WebApi.Models.Post;
using WPSUR.WebApi.Models.Tags;
using WPSUR.Services.Exceptions.PostExceptions;

namespace WPSUR.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ApiControllerBase
    {
        private readonly IPostService _postService;
        private readonly IMainTagService _mainTagService;

        public PostController(IPostService postService, IMainTagService mainTagService)
        {
            _postService = postService ?? throw new ArgumentNullException(nameof(postService));
            _mainTagService = mainTagService ?? throw new ArgumentNullException(nameof(mainTagService));
        }

        [Authorize]
        [HttpGet("findMainTagByTitle")]
        public async Task<ActionResult<ICollection<MainTagResponse>>> FindMainTagByTitle(string title)
        {
            try
            {
                ICollection<MainTagModel> mainTagModels = await _mainTagService.FindMainTagModelAsync(title);
                    
                ICollection<MainTagResponse> result = mainTagModels.Select(mainTag => new MainTagResponse()
                {
                    Title = mainTag.Title,
                    Id = mainTag.Id,
                }).ToList();  

                return Ok(mainTagModels);
            }
            catch(Exception)
            {
                return BadRequest("An unknown error occurred. Try again.");
            }
        }

        [Authorize]
        [HttpPost("CreatePost")]
        public async Task<IActionResult> CreatePost([FromBody] PostRequest postData)
        {
            var postModel = new PostModel()
            {
                Title = postData.Title,
                Body = postData.Body,
                MainTag = new MainTagModel() { Title = postData.MainTag, Id = new Guid() },
                SubTags = postData.SubTags.Select(subTag => new SubTagModel() { Title = subTag, Id = new Guid() }).ToList(),
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
        [HttpGet("GetMainTags")]
        public async Task<ActionResult<ICollection<MainTagResponse>>> GetMainTags()
        {
            try
            {
                ICollection<MainTagModel> mainTagsModels = await _mainTagService.ReceiveMainTags();
                ICollection<MainTagResponse> mainTagResponses = new List<MainTagResponse>();
                foreach (MainTagModel mainTagModel in mainTagsModels)
                {
                    MainTagResponse mainTagResponse = new()
                    {
                        Id = mainTagModel.Id,
                        Title = mainTagModel.Title,
                    };
                    mainTagResponses.Add(mainTagResponse);
                }
                return Ok(mainTagResponses);
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
        [HttpGet("GetMainTagState")]
        public async Task<ActionResult<MainTagStateResponse>> GetMainTagState([FromQuery] Guid mainTagId)
        {
            try
            {
                MainTagState mainTagState = await _mainTagService.ReceiveMainTagState(mainTagId);
                MainTagStateResponse mainTagResponse = new()
                {
                    Id = mainTagState.Id,
                    Title = mainTagState.Title,
                    SubTags = mainTagState.SubTags.Select(subTag => new SubTagStateResponse() { Id = subTag.Id, Title = subTag.Title }).ToList(),
                    Posts = mainTagState.Posts.Select(post => new PostStateResponse() { Id = post.Id, Title = post.Title, Body = post.Body }).ToList(),
                }; 
                return Ok(mainTagResponse);
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
        [HttpGet("GetPosts")]
        public async Task<ActionResult<ICollection<PostResponse>>> GetPosts()
        {
            try
            {
                ICollection<PostModel> posts = await _postService.ReceivePosts();

                ICollection<PostResponse> postsResponse = posts.Select(post => new PostResponse
                {
                    Id = post.Id,
                    Body = post.Body,
                    Title = post.Title,
                    CreatedBy = post.UserId,
                    MainTag = post.MainTag,
                    SubTags = post.SubTags,
                    Comments = post.Comments != null ? post.Comments.Select(comment => new CommentResponse()
                    {
                        Id = comment.Id,
                        Content = comment.Content,
                        CreatedDate = comment.CreatedDate,
                        CreatedBy = new UserResponse()
                        {
                            Id = comment.CreatedBy.Id,
                            LastName = comment.CreatedBy.LastName,
                            FirstName = comment.CreatedBy.FirstName,
                            Email = comment.CreatedBy.Email,
                        },
                    }).ToList() : null
                }).ToList();

                return Ok(postsResponse);
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
