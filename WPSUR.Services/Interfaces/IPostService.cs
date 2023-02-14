using WPSUR.Services.Models.Post;

namespace WPSUR.Services.Interfaces
{
    public interface IPostService
    {
        public Task CreatePostAsync(PostModel post);
        public Task<ICollection<PostModel>> ReceivePostsByMainTagAndSubTagAsync(Guid MainTagId, Guid SubTagId);
        public Task<ICollection<PostModel>> ReceivePostsByTitleAsync(string title);
    }
}
