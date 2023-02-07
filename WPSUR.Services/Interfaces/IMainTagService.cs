using WPSUR.Repository.Entities;

namespace WPSUR.Services.Interfaces
{
    public interface IMainTagService
    {
        public Task<MainTagEntity> GetOrCreateMainTagAsync(string mainTagTitle);
        public Task<MainTagEntity> AddPostToMainTagAsync(PostEntity post, MainTagEntity mainTag);
        public Task<MainTagEntity> AddSubTagsToMainTagAsync(ICollection<SubTagEntity> subTags, MainTagEntity mainTag);
    }
}
