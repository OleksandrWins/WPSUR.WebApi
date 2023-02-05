using WPSUR.Repository.Entities;
using WPSUR.Services.Models.Post;

namespace WPSUR.Services.Interfaces
{
    public interface IMainTagService
    {
        public Task<MainTagEntity> GetOrCreateMainTagAsync(PostModel postModel);
        public Task<MainTagEntity> AddPostToMainTag(PostEntity post, MainTagEntity mainTag);
        public Task<MainTagEntity> AddSubTagToMainTag(SubTagEntity subTag, MainTagEntity mainTag);
    }
}
