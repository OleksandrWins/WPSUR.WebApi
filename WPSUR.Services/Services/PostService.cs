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
            MainTagEntity mainTagEntity = await _mainTagRepository.GetMainTagByTitleAsync(postModel.MainTag);
            if (mainTagEntity == null)
            {
                mainTagEntity = new MainTagEntity()
                {
                    Id = Guid.NewGuid(),
                    Title = postModel.MainTag.Trim().ToUpper(),
                    CreatedBy = new UserEntity() { Id = Guid.NewGuid() },
                    CreatedDate = DateTime.UtcNow,
                };
            }

            ICollection<SubTagEntity> subTagEntities = await _subTagRepository.GetSubTagsByNamesAsync(postModel.SubTags);
            var subTagsToAdd = postModel.SubTags.Where(xx => !subTagEntities.Any(xxx => xxx.Title == xx)).ToList().AsReadOnly();
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
