﻿using WPSUR.Repository.Entities;
using WPSUR.Repository.Interfaces;
using WPSUR.Services.Interfaces;

namespace WPSUR.Services.Services
{
    public class SubTagService : ISubTagService
    {
        private readonly ISubTagRepository _subTagRepository;
        public SubTagService(ISubTagRepository subTagRepository, IMainTagService mainTagService, IPostService postService)
        {
            _subTagRepository = subTagRepository;
        }
        public async Task<SubTagEntity> GetOrCreateSubTagAsync(string subTagTitle)
        {
            SubTagEntity subTag = await _subTagRepository.GetSubTagByTitleAsync(subTagTitle);
            if (subTag != null)
            {
                return subTag;
            }
            subTagTitle = subTagTitle.Trim();
            subTagTitle.ToUpper();
            SubTagEntity subTagEntity = new SubTagEntity()
            {
                Id = Guid.NewGuid(),
                Title = subTagTitle,
                CreatedDate = DateTime.UtcNow
            };
            return subTagEntity;
        }
    }
}
