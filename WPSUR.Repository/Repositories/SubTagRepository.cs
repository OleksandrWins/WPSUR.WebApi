using Microsoft.EntityFrameworkCore;
using WPSUR.Repository.Entities;
using WPSUR.Repository.Interfaces;

namespace WPSUR.Repository.Repositories
{
    public sealed class SubTagRepository : ManageableRepositoryBase<SubTagEntity>, ISubTagRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SubTagRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<SubTagEntity> GetSubTagByTitleAsync(string title)
        {
            try
            {
                SubTagEntity subTag = await _dbContext.Set<SubTagEntity>().FirstOrDefaultAsync(x => x.Title == title);

                return subTag ?? throw new NullReferenceException(nameof(subTag));
            }
            catch
            {
                throw;
            }
        }
    }
}
