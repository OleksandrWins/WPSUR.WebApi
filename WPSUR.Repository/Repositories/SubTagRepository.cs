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
        public async Task<IList<SubTagEntity>> GetExistedSubTagsCollectionAsync(ICollection<string> subTagsTitles)
        {
            try
            {
                List<SubTagEntity> subTags = await _dbContext.SubTags.Where(subTag => subTagsTitles.Contains(subTag.Title))
                                                                        .Select(subTag => subTag)
                                                                        .Include(subTag => subTag.MainTags)
                                                                        .Include(subTag => subTag.Posts)
                                                                        .ToListAsync();

                return subTags;
            }
            catch
            {
                throw;
            }
        }
    }
}
