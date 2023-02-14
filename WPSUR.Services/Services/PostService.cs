using WPSUR.Repository.Entities;
using WPSUR.Repository.Interfaces;
using WPSUR.Services.Exceptions.PostExceptions;
using WPSUR.Services.Interfaces;
using WPSUR.Services.Models.Post;
using WPSUR.Services.Models.Tags;

namespace WPSUR.Services.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMainTagRepository _mainTagRepository;
        private readonly ISubTagRepository _subTagRepository;
        private readonly IUserRepository _userRepository;

        public PostService(IPostRepository postRepository, IMainTagRepository mainTagRepository, ISubTagRepository subTagRepository, IUserRepository userRepository)
        {
            _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
            _mainTagRepository = mainTagRepository ?? throw new ArgumentNullException(nameof(mainTagRepository));
            _subTagRepository = subTagRepository ?? throw new ArgumentNullException(nameof(subTagRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(subTagRepository));
        }

        public async Task CreatePostAsync(PostModel postModel)
        {
            PostValidation(postModel);

            MainTagEntity mainTagEntity = await GetMainTag(postModel.Title, postModel.UserId);

            ICollection<SubTagEntity> subTagEntities = await _subTagRepository.GetSubTagsByNamesAsync(postModel.SubTags);
            ICollection<string> subTagsToAdd = postModel.SubTags.Where(subTagTitle => !subTagEntities.Any(subTag => subTag.Title == subTagTitle)).ToList().AsReadOnly();
            foreach (string subTag in subTagsToAdd)
            {
                subTagEntities.Add(new SubTagEntity()
                {
                    Id = Guid.NewGuid(),
                    Title = subTag.Trim().ToUpper(),
                    CreatedBy = await _userRepository.GetByIdAsync(postModel.UserId),
                    CreatedDate = DateTime.UtcNow,
                });
            }

            AddMainTagToSubTag(subTagEntities, mainTagEntity);

            PostEntity postEntity = new()
            {
                Id = Guid.NewGuid(),
                Title = postModel.Title,
                Body = postModel.Body,
                MainTag = mainTagEntity,
                SubTags = subTagEntities,
                CreatedBy = await _userRepository.GetByIdAsync(postModel.UserId),
                CreatedDate = DateTime.UtcNow,
            };
            await _postRepository.CreateAsync(postEntity);
        }

        public async Task<ICollection<PostModel>> ReceivePostsByMainTagAndSubTagAsync(Guid MainTagId, Guid SubTagId)
        {
            ICollection<PostEntity> posts = await _postRepository.GetByMainTagAndSubTagIdsAsync(MainTagId, SubTagId);
            if (posts.Count == 0)
            {
                throw new NullReferenceException("No posts found.");
            }

            ICollection<PostModel> postModels = new List<PostModel>();
            foreach (PostEntity post in posts)
            {
                PostModel postModel = new()
                {
                    Title = post.Title,
                    Body = post.Body,
                };
                postModels.Add(postModel);
            }
            return postModels;
        }

        public async Task<ICollection<PostModel>> ReceivePostsByTitleAsync(string title)
        {
            ICollection<PostEntity> posts = await _postRepository.GetByTitleAsync(title);
            if (posts.Count == 0)
            {
                throw new NullReferenceException("No posts found.");
            }

            ICollection<PostModel> postModels = new List<PostModel>();
            foreach (PostEntity post in posts)
            {
                PostModel postModel = new()
                {
                    Title = post.Title,
                    Body = post.Body,
                };
                postModels.Add(postModel);
            }
            return postModels;
        }

        private void PostValidation(PostModel postModel)
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
        }

        private async Task<MainTagEntity> GetMainTag(string mainTagTitle, Guid userId)
        {
            MainTagEntity mainTagEntity = await _mainTagRepository.GetMainTagByTitleAsync(mainTagTitle);
            if (mainTagEntity == null)
            {
                mainTagEntity = new MainTagEntity()
                {
                    Id = Guid.NewGuid(),
                    Title = mainTagTitle.Trim().ToUpper(),
                    CreatedBy = await _userRepository.GetByIdAsync(userId),
                    CreatedDate = DateTime.UtcNow,
                };
            }
            return mainTagEntity;
        }

        private void AddMainTagToSubTag(ICollection<SubTagEntity> subTagEntities, MainTagEntity mainTagEntity)
        {
            foreach (SubTagEntity subTagEntity in subTagEntities)
            {
                if (subTagEntity.MainTags is null)
                {
                    subTagEntity.MainTags = new List<MainTagEntity>();
                }
                if (!subTagEntity.MainTags.Contains(mainTagEntity))
                {
                    subTagEntity.MainTags.Add(mainTagEntity);
                }
            }
        }
    }
}
