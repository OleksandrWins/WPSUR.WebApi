using WPSUR.Repository.Entities;

namespace WPSUR.Services.Interfaces
{
    public interface ISubTagService
    {
        //public Task<ICollection<SubTagEntity>> GetExistingSubTags(ICollection<string> subTags);
        public Task<ICollection<SubTagEntity>> GetOrCreateSubTagsAsync(ICollection<string> subTagsTitles);

        //public Task<SubTagEntity> GetOrCreateSubTagAsync(string subTagTitle);
        //public Task<SubTagEntity> AddPostToSubTagsAsync(PostEntity post, ICollection<SubTagEntity> subTags);
        public Task<ICollection<SubTagEntity>> AddPostToSubTagsAsync(PostEntity post, ICollection<SubTagEntity> subTags);
        public Task<SubTagEntity> AddMainTagToSubTagsAsync(MainTagEntity mainTag, SubTagEntity subTag);
    }
}
