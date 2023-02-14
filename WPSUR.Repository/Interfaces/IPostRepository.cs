using WPSUR.Repository.Entities;

namespace WPSUR.Repository.Interfaces
{
    public interface IPostRepository : IManageableRepositoryBase<PostEntity>
    {
        public Task CreateAsync(PostEntity post);
        public Task<ICollection<PostEntity>> GetByMainTagAndSubTagIdsAsync(Guid MainTagId, Guid SubTagId);
        public Task<ICollection<PostEntity>> GetByTitleAsync(string title);
    }
}
