using Microsoft.EntityFrameworkCore;
using WPSUR.Repository.Entities;
using WPSUR.Repository.Interfaces;

namespace WPSUR.Repository.Repositories
{
    public sealed class MainTagRepository : ManageableRepositoryBase<MainTagEntity>, IMainTagRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public MainTagRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<MainTagEntity> GetMainTagByTitleAsync(string title)
        {
            try
            {
                MainTagEntity mainTag = await _dbContext.Set<MainTagEntity>().FirstOrDefaultAsync(x => x.Title == title);

                return mainTag; //?? throw new NullReferenceException(nameof(mainTag));
            }
            catch
            {
                throw;
            }
        }
    }
}
