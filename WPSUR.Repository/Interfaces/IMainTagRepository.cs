using WPSUR.Repository.Entities;

namespace WPSUR.Repository.Interfaces
{
    public interface IMainTagRepository : IManageableRepositoryBase<MainTagEntity>
    {
        public Task<MainTagEntity> GetMainTagByTitleAsync(string title);
    }
}
