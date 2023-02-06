using WPSUR.Repository.Entities;
using WPSUR.Repository.Interfaces;
using WPSUR.Services.Interfaces;

namespace WPSUR.Services.Services
{
    public class MainTagService : IMainTagService
    {
        private readonly IMainTagRepository _mainTagRepository;

        public MainTagService(IMainTagRepository mainTagRepository)
        {
            _mainTagRepository = mainTagRepository ?? throw new ArgumentNullException(nameof(mainTagRepository));
        }

        public async Task<MainTagEntity> GetOrCreateMainTagAsync(string mainTagTitle)
        {
            MainTagEntity mainTag = await _mainTagRepository.GetMainTagByTitleAsync(mainTagTitle);
            if (mainTag != null)
            {
                return mainTag;
            }
            if (string.IsNullOrWhiteSpace(mainTagTitle))
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
