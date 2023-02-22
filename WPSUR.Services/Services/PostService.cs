using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using WPSUR.Repository.Entities;
using WPSUR.Repository.Interfaces;
using WPSUR.Repository.Repositories;
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

        public PostService(IPostRepository postRepository, IMainTagRepository mainTagRepository, ISubTagRepository subTagRepository)
        {
            _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
            _mainTagRepository = mainTagRepository ?? throw new ArgumentNullException(nameof(mainTagRepository));
            _subTagRepository = subTagRepository ?? throw new ArgumentNullException(nameof(subTagRepository));
        }

        public async Task CreatePostAsync(PostModel postModel)
        {
            PostValidation(postModel);

            MainTagEntity mainTagEntity = await GetMainTag(postModel.MainTag.Title, postModel.UserId);

            ICollection<SubTagEntity> subTagEntities = await _subTagRepository.GetSubTagsByNamesAsync(postModel.SubTags.Select(subTag => subTag.Title).ToList());
            ICollection<string> subTagsToAdd = postModel.SubTags.Where(subTagTitle => !subTagEntities.Any(subTag => subTag.Title == subTagTitle.Title)).Select(subTag => subTag.Title).ToList().AsReadOnly();
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

        public async Task<ICollection<PostModel>> ReceivePosts()
        {
            ICollection<PostEntity> posts = await _postRepository.GetPostsAsync();
            if (posts.Count == 0)
            {
                throw new NullReferenceException("No posts found.");
            }

            ICollection<PostModel> postModels = posts.Select(post => new PostModel()
            {
                Id = post.Id,
                UserId = post.CreatedBy.Id,
                Title = post.Title,
                Body = post.Body,
                Comments = post.Comments.Select(comment => new CommentModel()
                {
                    Content = comment.Content,
                    CreatedBy = new UserModel() { Email = comment.CreatedBy.Email, FirstName = comment.CreatedBy.FirstName, Id = comment.CreatedBy.Id, LastName = comment.CreatedBy.LastName},
                    CreatedDate = comment.CreatedDate,
                    Id = comment.Id,
                }).ToList(),
                MainTag = new MainTagModel() { Id = post.MainTag.Id, Title = post.MainTag.Title },
                SubTags = post.SubTags.Select(subTag => new SubTagModel() { Id = subTag.Id, Title = subTag.Title }).ToList(),
            }).ToList();
           
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

        private async Task<MainTagEntity> GetMainTag(string mainTagTitle)
        {
            MainTagEntity mainTagEntity = await _mainTagRepository.GetMainTagByTitleAsync(mainTagTitle);
            if (mainTagEntity == null)
            {
                mainTagEntity = new MainTagEntity()
                {
                    Id = Guid.NewGuid(),
                    Title = mainTagTitle.Trim().ToUpper(),
                    CreatedBy = new UserEntity() { Id = Guid.NewGuid() },
                    CreatedDate = DateTime.UtcNow,
                };
            }
            return mainTagEntity;
        }

        private async Task GetMainTagSubEntities(ICollection<SubTagEntity> subTagEntities, MainTagEntity mainTagEntity)
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
        public async Task CreatePostAsync(PostModel postModel)
        {
            PostValidationException(postModel);

            MainTagEntity mainTagEntity = await GetMainTag(postModel.Title);

            ICollection<SubTagEntity> subTagEntities = await _subTagRepository.GetSubTagsByNamesAsync(postModel.SubTags);
            ICollection<string> subTagsToAdd = postModel.SubTags.Where(subTagTitle => !subTagEntities.Any(subTag => subTag.Title == subTagTitle)).ToList().AsReadOnly();
            foreach(string subTag in subTagsToAdd)
            {
                subTagEntities.Add(new SubTagEntity()
                {
                    Id = Guid.NewGuid(),
                    Title = subTag.Trim().ToUpper(),
                    CreatedBy = new UserEntity() { Id = Guid.NewGuid() },
                    CreatedDate = DateTime.UtcNow,
                }) ;
            }

            await GetMainTagSubEntities(subTagEntities, mainTagEntity);

            PostEntity postEntity = new()
            {
                Id = Guid.NewGuid(),
                Title = postModel.Title,
                Body = postModel.Body,
                MainTag = mainTagEntity,
                SubTags = subTagEntities,
                CreatedBy = new UserEntity() { Id = Guid.NewGuid()},
                CreatedDate = DateTime.UtcNow,
            };
            await _postRepository.CreateAsync(postEntity);
        }
    }
}
