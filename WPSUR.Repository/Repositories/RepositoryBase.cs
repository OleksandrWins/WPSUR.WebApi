using Microsoft.EntityFrameworkCore;
using WPSUR.Repository.Entities;
using WPSUR.Repository.Interfaces;

namespace WPSUR.Repository.Repositories
{
    public abstract class RepositoryBase<T> : IRepositotyBase<T> where T : EntityBase
    {
        private readonly ApplicationDbContext _dbContext;

        public RepositoryBase(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            try
            {
                T entity = await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);

                return entity ?? throw new NullReferenceException(nameof(entity));
            }
            catch
            {
                throw;
            }
        }
    }
}
