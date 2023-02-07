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

        public PostService(IPostRepository postRepository, ISubTagService subTagService, IMainTagService mainTagService)
        {
            _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository)); 
            _subTagService = subTagService ?? throw new ArgumentNullException(nameof(subTagService));
            _mainTagService = mainTagService ?? throw new ArgumentNullException(nameof(mainTagService));
        }

        public async Task CreatePostAsync(PostModel postModel)
        {
            if (String.IsNullOrWhiteSpace(postModel.Title))
            {
                throw new NullReferenceException("The post title is empty.");
            }
            if (postModel.Title.Length > 50)
            {
                throw new LengthOfTitleException("The title of the post should not exceed 50 symbols.");
            }
            if (String.IsNullOrWhiteSpace(postModel.Body))
            {
                throw new NullReferenceException("The body of the post is empty.");
            }
            if (postModel.Body.Length > 1000)
            {
                throw new LengthOfBodyException("The body of the post should not exceed 1000 symbols.");
            }

            PostEntity postEntity = new PostEntity()
            {
                Id = Guid.NewGuid(),
                Title = postModel.Title,
                Body = postModel.Body,
                MainTag = await _mainTagService.GetOrCreateMainTagAsync(postModel.MainTag),
                SubTags = await _subTagService.GetOrCreateSubTagsAsync(postModel.SubTags)
            };
            await _mainTagService.AddPostToMainTagAsync(postEntity, postEntity.MainTag);
            await _mainTagService.AddSubTagsToMainTagAsync(postEntity.SubTags, postEntity.MainTag);
            await _subTagService.AddPostToSubTagsAsync(postEntity, postEntity.SubTags);
            await _subTagService.AddMainTagToSubTagsAsync(postEntity.MainTag, postEntity.SubTags);
            await _postRepository.SaveNewPostAsync(postEntity);
        }
    }
}
