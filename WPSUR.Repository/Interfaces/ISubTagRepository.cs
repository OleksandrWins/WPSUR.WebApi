using WPSUR.Repository.Entities;

namespace WPSUR.Repository.Interfaces
{
    public interface ISubTagRepository : IManageableRepositoryBase<SubTagEntity>
    {
        public Task<ICollection<SubTagEntity>> GetSubTagsByNamesAsync(ICollection<string> subTagsTitles);
    }
}
