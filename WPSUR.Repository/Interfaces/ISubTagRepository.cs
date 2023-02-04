using WPSUR.Repository.Entities;

namespace WPSUR.Repository.Interfaces
{
    public interface ISubTagRepository : IManageableRepositoryBase<SubTagEntity>
    {
        public Task<SubTagEntity> GetSubTagByTitleAsync(string title);
    }
}
