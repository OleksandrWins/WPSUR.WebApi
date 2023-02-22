using WPSUR.Services.Models.Account;
using WPSUR.Services.Models.Comment.Request;

namespace WPSUR.Services.Interfaces
{
    public interface ICommentService
    {
        Task<UserModel> CreateCommentAsync(CreateCommentRequest createComment, Guid commentId);
    }
}