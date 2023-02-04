﻿using WPSUR.Repository.Entities;

namespace WPSUR.Repository.Interfaces
{
    public interface IPostRepository : IManageableRepositoryBase<PostEntity>
    {
        public Task SaveNewPostAsync(PostEntity post);
        public Task<PostEntity> GetPostByTitleAsync(string title);
        public Task<PostEntity> GetPostByBodyAsync(string body);
    }
}
