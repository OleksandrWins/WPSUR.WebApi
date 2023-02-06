using WPSUR.Repository.Entities;
using WPSUR.Repository.Interfaces;
using WPSUR.Services.Interfaces;
using WPSUR.Services.Models.Post;

namespace WPSUR.Services.Services
{
    public class MainTagService : IMainTagService
    {
        private readonly IMainTagRepository _mainTagRepository;


        public MainTagService(IMainTagRepository mainTagRepository, ISubTagService subTagService, IPostService postService)
        {
            _mainTagRepository = mainTagRepository;
        }

        public async Task<MainTagEntity> GetOrCreateMainTagAsync(PostModel postModel)
        {
            MainTagEntity mainTag = await _mainTagRepository.GetMainTagByTitleAsync(postModel.MainTag);
            if (mainTag != null)
            {
                return mainTag;
            }
            if (postModel.MainTag==null)
            {
                throw new NullReferenceException("The main tag is empty.");
            }
            postModel.MainTag = postModel.MainTag.Trim();
            postModel.MainTag.ToUpper();
            MainTagEntity mainTagEntity = new MainTagEntity()
            {
                Id = Guid.NewGuid(),
                Title = postModel.MainTag,
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
