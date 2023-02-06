using WPSUR.Repository.Entities;

namespace WPSUR.Services.Interfaces
{
    public interface ISubTagService
    {
        public Task<SubTagEntity> GetOrCreateSubTagAsync(string subTagTitle);
        public Task<SubTagEntity> AddPostToSubTagAsync(PostEntity post, SubTagEntity subTag);
        public Task<SubTagEntity> AddMainTagToSubTagAsync(MainTagEntity mainTag, SubTagEntity subTag);
    }
}
