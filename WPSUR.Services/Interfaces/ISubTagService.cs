using WPSUR.Repository.Entities;

namespace WPSUR.Services.Interfaces
{
    public interface ISubTagService
    {
        public Task<SubTagEntity> GetOrCreateSubTagAsync(string subTagTitle);
        public Task<SubTagEntity> AddPostToSubTag(PostEntity post, SubTagEntity subTag);
        public Task<SubTagEntity> AddMainTagToSubTag(MainTagEntity mainTag, SubTagEntity subTag);
    }
}
