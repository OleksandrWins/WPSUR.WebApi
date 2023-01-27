using WPSUR.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace WPSUR.Repository.Interfaces
{
    public abstract class ManageableRepositoryBase<T> : IManageableRepositoryBase<T> where T: ManageableEntityBase
    {
        private readonly ApplicationDbContext _dbContext;

        public ManageableRepositoryBase(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public virtual async Task DeleteAsyncBase(T manageableEntity)
        {
            try
            {
                _dbContext.Set<T>().Update(manageableEntity);

                manageableEntity.DeletedDate = DateTime.UtcNow;

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            try
            {
                T? managableEntity = await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);

                return managableEntity ?? throw new NullReferenceException(nameof(managableEntity));
            }
            catch
            {
                throw;
            }
        }
    }
}
