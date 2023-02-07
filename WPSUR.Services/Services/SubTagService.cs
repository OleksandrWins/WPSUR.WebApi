using WPSUR.Repository.Entities;
using WPSUR.Repository.Interfaces;
using WPSUR.Services.Interfaces;

namespace WPSUR.Services.Services
{
    public class SubTagService : ISubTagService
    {
        private readonly ISubTagRepository _subTagRepository;
        public SubTagService(ISubTagRepository subTagRepository)
        {
            _subTagRepository = subTagRepository ?? throw new ArgumentNullException(nameof(subTagRepository));
        }
        //public async Task/*<ICollection<SubTagEntity>>*/ GetExistingSubTags(ICollection<string> subTagsTitles)
        //{
        //    ICollection<SubTagEntity> existingSubTags = new HashSet<SubTagEntity>();
        //    //ICollection<SubTagEntity> noExistingSubTags = new HashSet<SubTagEntity>();
        //    foreach (var subTagTitle in subTagsTitles) 
        //    {
        //        SubTagEntity subTag = await _subTagRepository.GetSubTagByTitleAsync(subTagTitle);
        //        if (subTag != null)
        //        {
        //            existingSubTags.Add(subTag);
        //            subTagsTitles.Remove(subTagTitle);
        //        }
        //        //noExistingSubTags.Add(subTag);
        //    }
        //    await CreateSubTags(subTagsTitles, existingSubTags);
        //    //return existingSubTags;
        //}
        //private async Task<ICollection<SubTagEntity>> CreateSubTags(ICollection<string> subTagsTitles, ICollection<SubTagEntity> subTags)
        //{
        //    foreach(var subTagTitle in subTagsTitles) 
        //    {
        //        if (string.IsNullOrWhiteSpace(subTagTitle))
        //        {
        //            throw new NullReferenceException("The sub tag is empty.");
        //        }
        //        /*subTagTitle = */subTagTitle.Trim();
        //        subTagTitle.ToUpper();
        //        SubTagEntity subTagEntity = new SubTagEntity()
        //        {
        //            Id = Guid.NewGuid(),
        //            Title = subTagTitle,
        //            CreatedDate = DateTime.UtcNow
        //        };
        //        subTags.Add(subTagEntity);
        //    }
        //    return subTags;
        //}
        public async Task<ICollection<SubTagEntity>> GetOrCreateSubTagsAsync(ICollection<string> subTagsTitles)
        {
            List<SubTagEntity> subTags = (List<SubTagEntity>)await _subTagRepository.GetExistedSubTagsCollectionAsync(subTagsTitles);
            foreach (var subTag in subTags)
            {
                foreach (var subTagTitle in subTagsTitles)
                {
                    if (subTag.Title != subTagTitle)
                    {
                        if (string.IsNullOrWhiteSpace(subTagTitle))
                        {
                            throw new NullReferenceException("The sub tag is empty.");
                        }
                        subTagTitle.Trim();
                        subTagTitle.ToUpper();
                        SubTagEntity subTagEntity = new SubTagEntity()
                        {
                            Id = Guid.NewGuid(),
                            Title = subTagTitle,
                            CreatedDate = DateTime.UtcNow
                        };
                        subTags.Add(subTagEntity);
                    }
                }
            }
            return subTags;
            //SubTagEntity subTag = await _subTagRepository.GetSubTagByTitleAsync(subTagTitle);
            //if (subTag != null)
            //{
            //    return subTag;
            //}
            //if (string.IsNullOrWhiteSpace(subTagTitle))
            //{
            //    throw new NullReferenceException("The sub tag is empty.");
            //}
            //subTagTitle = subTagTitle.Trim();
            //subTagTitle.ToUpper();
            //SubTagEntity subTagEntity = new SubTagEntity()
            //{
            //    Id = Guid.NewGuid(),
            //    Title = subTagTitle,
            //    CreatedDate = DateTime.UtcNow
            //};
            //return subTagEntity;
        }
        public async Task<ICollection<SubTagEntity>> AddPostToSubTagsAsync(PostEntity post, ICollection<SubTagEntity> subTags)
        {
            foreach(var subTag in subTags)
            {
               subTag.Posts.Add(post);
            }
            return subTags;
        }
        public async Task<SubTagEntity> AddMainTagToSubTagsAsync(MainTagEntity mainTag, SubTagEntity subTag)
        {
            subTag.MainTags.Add(mainTag);
            return subTag;
        }
    }
}
