using Microsoft.IdentityModel.Tokens;
using WPSUR.Repository.Entities;
using WPSUR.Repository.Interfaces;
using WPSUR.Services.Exceptions.PostExceptions;
using WPSUR.Services.Interfaces;
using WPSUR.Services.Models.Post;

namespace WPSUR.Services.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly ISubTagService _subTagService;
        private readonly IMainTagService _mainTagService;
        private readonly IValidationPostService _validationPostService;

        public PostService(IPostRepository postRepository, ISubTagService subTagService, IMainTagService mainTagService)
        {
            _postRepository = postRepository;
            _subTagService = subTagService;
            _mainTagService = mainTagService;
        }

        public async Task CreatePostAsync(PostModel postModel)
        {
            _validationPostService.ValidatePost(postModel);

            PostEntity postEntity = new PostEntity()
            {
                Id = Guid.NewGuid(),
                Title = postModel.Title,
                Body = postModel.Body,
                MainTag = await _mainTagService.GetOrCreateMainTagAsync(postModel),
            };
            foreach (string subTagTitle in postModel.SubTags)
            {
                postEntity.SubTags.Add(await _subTagService.GetOrCreateSubTagAsync(subTagTitle));
            }
            try
            {
                await _postRepository.SaveNewPostAsync(postEntity);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
