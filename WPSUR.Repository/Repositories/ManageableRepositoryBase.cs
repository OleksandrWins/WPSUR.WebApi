using WPSUR.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using WPSUR.Repository.Interfaces;

namespace WPSUR.Repository.Repositories
{
    public abstract class ManageableRepositoryBase<T> : IManageableRepositoryBase<T> where T : ManageableEntityBase
    {
        private readonly ApplicationDbContext _dbContext;

        public ManageableRepositoryBase(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task DeleteAsync(T manageableEntity)
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

        public async Task<T> GetByIdAsync(Guid id)
        {
            try
            {
                T managableEntity = await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);

                return managableEntity ?? throw new NullReferenceException(nameof(managableEntity));
            }
            catch
            {
                throw;
            }
        }
    }
}
