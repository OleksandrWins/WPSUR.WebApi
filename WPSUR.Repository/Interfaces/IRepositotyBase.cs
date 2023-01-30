using WPSUR.Repository.Entities;

namespace WPSUR.Repository.Interfaces
{
    public interface IRepositotyBase<T> where T : EntityBase
    {
        public Task<T> GetByIdAsync(Guid id);
    }
}
