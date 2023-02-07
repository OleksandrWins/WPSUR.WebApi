using WPSUR.Repository.Entities;
using WPSUR.Repository.Interfaces;
using WPSUR.Repository.Migrations;
using WPSUR.Services.Interfaces;

namespace WPSUR.Services.Services
{
    public class SubTagService : ISubTagService
    {
        private readonly ISubTagRepository _subTagRepository;
        public SubTagService(ISubTagRepository subTagRepository)
        {
            _subTagRepository = subTagRepository ?? throw new ArgumentNullException(nameof(subTagRepository));
        }
        public async Task<ICollection<SubTagEntity>> GetOrCreateSubTagsAsync(ICollection<string> subTagsTitles)
        {
            List<SubTagEntity> subTags = (List<SubTagEntity>)await _subTagRepository.GetExistedSubTagsCollectionAsync(subTagsTitles);
            foreach (var subTag in subTags)
            {
                foreach (var subTagTitle in subTagsTitles)
                {
                    if (subTag.Title != subTagTitle)
                    {
                        if (string.IsNullOrWhiteSpace(subTagTitle))
                        {
                            throw new NullReferenceException("The sub tag is empty.");
                        }
                        subTagTitle.Trim();
                        subTagTitle.ToUpper();
                        SubTagEntity subTagEntity = new SubTagEntity()
                        {
                            Id = Guid.NewGuid(),
                            Title = subTagTitle,
                            CreatedDate = DateTime.UtcNow
                        };
                        subTags.Add(subTagEntity);
                    }
                }
            }
            return subTags;
        }
        public async Task<ICollection<SubTagEntity>> AddPostToSubTagsAsync(PostEntity post, ICollection<SubTagEntity> subTags)
        {
            foreach(var subTag in subTags)
            {
               subTag.Posts.Add(post);
            }
            return subTags;
        }
        public async Task<ICollection<SubTagEntity>> AddMainTagToSubTagsAsync(MainTagEntity mainTag, ICollection<SubTagEntity> subTags)
        {
            foreach (var subTag in subTags)
            {
                subTag.MainTags.Add(mainTag);
            }
            return subTags;
        }
    }
}
