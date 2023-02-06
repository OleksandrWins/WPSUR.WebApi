using WPSUR.Repository.Entities;
using WPSUR.Repository.Interfaces;
using WPSUR.Services.Interfaces;
using WPSUR.Services.Models.Post;

namespace WPSUR.Services.Services
{
    public class MainTagService : IMainTagService
    {
        private readonly IMainTagRepository _mainTagRepository;


        public MainTagService(IMainTagRepository mainTagRepository)
        {
            if (mainTagRepository==null)
            {
                throw new NullReferenceException("An error occurred.");
            }
            _mainTagRepository = mainTagRepository;
        }

        public async Task<MainTagEntity> GetOrCreateMainTagAsync(string mainTagTitle)
        {
            MainTagEntity mainTag = await _mainTagRepository.GetMainTagByTitleAsync(mainTagTitle);
            if (mainTag != null)
            {
                return mainTag;
            }
            if (mainTagTitle == null)
            {
                throw new NullReferenceException("The main tag is empty.");
            }
            mainTagTitle = mainTagTitle.Trim();
            mainTagTitle.ToUpper();
            MainTagEntity mainTagEntity = new MainTagEntity()
            {
                Id = Guid.NewGuid(),
                Title = mainTagTitle,
                CreatedDate = DateTime.UtcNow
            };
            return mainTagEntity;
        }
        public async Task<MainTagEntity> AddPostToMainTagAsync(PostEntity post, MainTagEntity mainTag)
        {
            mainTag.Posts.Add(post);
            return mainTag;
        }
        public async Task<MainTagEntity> AddSubTagToMainTagAsync(SubTagEntity subTag, MainTagEntity mainTag)
        {
            mainTag.SubTags.Add(subTag);
            return mainTag;
        }
    }
}
