using Microsoft.AspNetCore.Mvc;
using WPSUR.Services.Interfaces;
using WPSUR.Services.Models.Post;
using WPSUR.WebApi.Models.Post;

namespace WPSUR.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService ?? throw new ArgumentNullException(nameof(postService));
        }

        [HttpPost("CreatePost")]
        public async Task<IActionResult> CreatePost([FromBody] PostRequest postData)
        {
            var postModel = new PostModel()
            {
                Title = postData.Title,
                Body = postData.Body,
                MainTag = postData.MainTag,
                SubTags = postData.SubTags,
            };
            try
            {
                await _postService.CreatePostAsync(postModel);
                return Ok();
            }
            catch (NotImplementedException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
