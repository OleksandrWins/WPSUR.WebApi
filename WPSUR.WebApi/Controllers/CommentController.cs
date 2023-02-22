using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WPSUR.Services.Interfaces;
using WPSUR.Services.Models.Account;
using WPSUR.Services.Models.Comment.Request;
using WPSUR.WebApi.Models.Comment;
using WPSUR.WebApi.Models.Post;

namespace WPSUR.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ApiControllerBase
    {
        private readonly ICommentService _commentService; 

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost("createComment")]
        public async Task<ActionResult<CommentResponse>> CreateComment(CreateRequests commentRequests)
        {
            try
            {
                if (commentRequests == null)
                {
                    return BadRequest("Invalid data");
                }

                CreateCommentRequest createComment = new()
                {
                    Content = commentRequests.Content,
                    TargetPostId = commentRequests.TargetPost,
                    CreatorId = LoggedInUserId,
                };

                Guid commentId = Guid.NewGuid();

                UserModel creator = await _commentService.CreateCommentAsync(createComment, commentId);

                CommentResponse response = new()
                {
                    Id = commentId,
                    Content = commentRequests.Content,
                    CreatedDate = DateTime.Now,
                    CreatedBy = new() { Email= creator.Email, Id = creator.Id, LastName = creator.LastName, FirstName = creator.FirstName },
                };

                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong. Try again later.");
            }
        }
    }
}
