using WPSUR.Repository.Entities;
using WPSUR.Services.Models.Post;

namespace WPSUR.Services.Interfaces
{
    public interface IMainTagService
    {
        public Task<MainTagEntity> GetOrCreateMainTagAsync(PostModel postModel);
        public Task<MainTagEntity> AddPostToMainTagAsync(PostEntity post, MainTagEntity mainTag);
        public Task<MainTagEntity> AddSubTagToMainTagAsync(SubTagEntity subTag, MainTagEntity mainTag);
    }
}
