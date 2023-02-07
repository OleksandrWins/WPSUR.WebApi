using WPSUR.Repository.Entities;

namespace WPSUR.Repository.Interfaces
{
    public interface ISubTagRepository : IManageableRepositoryBase<SubTagEntity>
    {
        public Task<IList<SubTagEntity>> GetExistedSubTagsCollectionAsync(ICollection<string> subTagsTitles);
    }
}
