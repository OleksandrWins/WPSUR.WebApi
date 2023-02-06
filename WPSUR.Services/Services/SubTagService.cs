using WPSUR.Repository.Entities;
using WPSUR.Repository.Interfaces;
using WPSUR.Repository.Repositories;
using WPSUR.Services.Interfaces;
using WPSUR.Services.Models.Post;

namespace WPSUR.Services.Services
{
    public class SubTagService : ISubTagService
    {
        private readonly ISubTagRepository _subTagRepository;
        public SubTagService(ISubTagRepository subTagRepository)
        {
            if (subTagRepository == null)
            {
                throw new NullReferenceException("An error occurred.");
            }
            _subTagRepository = subTagRepository;
        }
        public async Task<SubTagEntity> GetOrCreateSubTagAsync(string subTagTitle)
        {
            SubTagEntity subTag = await _subTagRepository.GetSubTagByTitleAsync(subTagTitle);
            if (subTag != null)
            {
                return subTag;
            }
            if (subTagTitle == null)
            {
                throw new NullReferenceException("The sub tag is empty.");
            }
            subTagTitle = subTagTitle.Trim();
            subTagTitle.ToUpper();
            SubTagEntity subTagEntity = new SubTagEntity()
            {
                Id = Guid.NewGuid(),
                Title = subTagTitle,
                CreatedDate = DateTime.UtcNow
            };
            return subTagEntity;
        }
        public async Task<SubTagEntity> AddPostToSubTagAsync(PostEntity post, SubTagEntity subTag)
        {
            subTag.Posts.Add(post);
            return subTag;
        }
        public async Task<SubTagEntity> AddMainTagToSubTagAsync(MainTagEntity mainTag, SubTagEntity subTag)
        {
            subTag.MainTags.Add(mainTag);
            return subTag;
        }
    }
}
