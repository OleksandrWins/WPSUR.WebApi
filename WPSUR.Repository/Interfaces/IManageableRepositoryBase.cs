using WPSUR.Repository.Entities;

namespace WPSUR.Repository.Interfaces
{
    public interface IManageableRepositoryBase<T> : IRepositotyBase<T> where T : ManageableEntityBase
    {
        public Task DeleteAsync(T manageableEntity);
    }
}
