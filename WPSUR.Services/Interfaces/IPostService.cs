using WPSUR.Services.Models.Post;
using WPSUR.Services.Models.Tags;

namespace WPSUR.Services.Interfaces
{
    public interface IPostService
    {
        public Task CreatePostAsync(PostModel post);
        public Task<ICollection<PostModel>> ReceivePostsByMainTagAndSubTagAsync(Guid MainTagId, Guid SubTagId);
        public Task<ICollection<PostModel>> ReceivePostsByTitleAsync(string title);
        public Task<ICollection<PostModel>> ReceivePosts();
    }
}
