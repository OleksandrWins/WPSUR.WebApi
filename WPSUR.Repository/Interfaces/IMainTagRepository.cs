using WPSUR.Repository.Entities;

namespace WPSUR.Repository.Interfaces
{
    public interface IMainTagRepository : IManageableRepositoryBase<MainTagEntity>
    {
        Task<ICollection<MainTagEntity>> FindMainTagByTitleAsync(string title);
        public Task<MainTagEntity> GetMainTagByTitleAsync(string title);
        public Task<ICollection<MainTagEntity>> GetMainTagsAsync();
        public Task<MainTagEntity> GetMainTagState(Guid MainTagId);
    }
}
