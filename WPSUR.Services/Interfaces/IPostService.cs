using WPSUR.Services.Models.Post;

namespace WPSUR.Services.Interfaces
{
    public interface IPostService
    {
        public Task CreatePostAsync(PostModel post);
    }
}
