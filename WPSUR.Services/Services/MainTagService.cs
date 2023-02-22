using WPSUR.Repository.Entities;
using WPSUR.Repository.Interfaces;
using WPSUR.Services.Interfaces;
using WPSUR.Services.Models.Post;
using WPSUR.Services.Models.Tags;

namespace WPSUR.Services.Services
{
    public class MainTagService: IMainTagService
    {
        private readonly IMainTagRepository _mainTagRepository;
        public MainTagService(IMainTagRepository mainTagRepository)
        {   
            _mainTagRepository = mainTagRepository ?? throw new ArgumentNullException(nameof(mainTagRepository));
        }

        public async Task<ICollection<MainTagModel>> FindMainTagModelAsync(string title)
        {
            ICollection<MainTagEntity> mainTags = await _mainTagRepository.FindMainTagByTitleAsync(title);

            ICollection<MainTagModel> mainTagModels = mainTags.Select(mainTag => new MainTagModel()
            {
                Id = mainTag.Id,
                Title = mainTag.Title,
            }).ToList();

            return mainTagModels;
        }

        public async Task<ICollection<MainTagModel>> ReceiveMainTags()
        {
            ICollection<MainTagEntity> mainTags = await _mainTagRepository.GetMainTagsAsync();
            ICollection<MainTagModel> mainTagModels = new List<MainTagModel>();
            foreach (MainTagEntity mainTag in mainTags)
            {
                MainTagModel mainTagModel = new()
                {
                    Id = mainTag.Id,
                    Title = mainTag.Title,
                };
                mainTagModels.Add(mainTagModel);
            }
            return mainTagModels;
        }

        public async Task<MainTagState> ReceiveMainTagState(Guid id)
        {
            MainTagEntity mainTagEntity = await _mainTagRepository.GetMainTagState(id);
            MainTagState mainTagState = new()
            {
                Id = mainTagEntity.Id,
                Title = mainTagEntity.Title,
                SubTags = mainTagEntity.SubTags.Select(subTag => new SubTagState() { Id = subTag.Id, Title = subTag.Title }).ToList(),
                Posts = mainTagEntity.Posts.Select(post => new PostState() { Id = post.Id, Title = post.Title, Body = post.Body, SubTags = post.SubTags.Select(subTag => new SubTagState() { Id = subTag.Id, Title = subTag.Title}).ToList()}).ToList(),
            };
            return mainTagState;
        }
    }
}
