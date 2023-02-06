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
            _postRepository = postRepository;
            _subTagService = subTagService;
            _mainTagService = mainTagService;
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
            };

            await _mainTagService.AddPostToMainTagAsync(postEntity, postEntity.MainTag);

            foreach (string subTagTitle in postModel.SubTags)
            {
                SubTagEntity subTag = await _subTagService.GetOrCreateSubTagAsync(subTagTitle);
                postEntity.SubTags.Add(subTag);
                await _subTagService.AddPostToSubTagAsync(postEntity, subTag);
                await _mainTagService.AddSubTagToMainTagAsync(subTag, postEntity.MainTag);
                await _subTagService.AddMainTagToSubTagAsync(postEntity.MainTag, subTag);
            }

            await _postRepository.SaveNewPostAsync(postEntity);
        }
    }
}
