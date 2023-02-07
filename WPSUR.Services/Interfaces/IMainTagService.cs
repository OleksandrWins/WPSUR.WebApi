using WPSUR.Repository.Entities;
using WPSUR.Services.Models.Post;

namespace WPSUR.Services.Interfaces
{
    public interface IMainTagService
    {
        public Task<MainTagEntity> GetOrCreateMainTagAsync(string mainTagTitle);
        public Task<MainTagEntity> AddPostToMainTagAsync(PostEntity post, MainTagEntity mainTag);
        public Task<MainTagEntity> AddSubTagsToMainTagAsync(SubTagEntity subTag, MainTagEntity mainTag);
    }
}
