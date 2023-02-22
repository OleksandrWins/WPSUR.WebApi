using WPSUR.Repository.Entities;

namespace WPSUR.Repository.Interfaces
{
    public interface ICommentRepository
    {
        Task CreateCommentAsync(CommentEntity comment);
        Task DeleteCommentAsync(CommentEntity commentEntity);
        Task UpdateCommentAsync(CommentEntity comment);
    }
}