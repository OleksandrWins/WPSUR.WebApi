using WPSUR.Repository.Entities;

namespace WPSUR.Services.Interfaces
{
    public interface ISubTagService
    {
        public Task<ICollection<SubTagEntity>> GetOrCreateSubTagsAsync(ICollection<string> subTagsTitles);
        public Task<ICollection<SubTagEntity>> AddPostToSubTagsAsync(PostEntity post, ICollection<SubTagEntity> subTags);
        public Task<ICollection<SubTagEntity>> AddMainTagToSubTagsAsync(MainTagEntity mainTag, ICollection<SubTagEntity> subTags);
    }
}
