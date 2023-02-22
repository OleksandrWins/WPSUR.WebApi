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

                return mainTag; 
            }
            catch
            {
                throw;
            }
        }
        public async Task<ICollection<MainTagEntity>> GetMainTagsAsync()
            => await _dbContext.MainTags.OrderByDescending(m => m.Posts.Count()).Take(5).ToListAsync();

        public async Task<MainTagEntity> GetMainTagState(Guid MainTagId)
            => await _dbContext.MainTags.Include(mainTag => mainTag.Posts).ThenInclude(post => post.SubTags).Include(mainTag => mainTag.SubTags).FirstOrDefaultAsync(mainTag => mainTag.Id == MainTagId);

        public async Task<ICollection<MainTagEntity>> FindMainTagByTitleAsync(string title)
            => await _dbContext.MainTags.Where(mainTag => mainTag.Title.Contains(title) || title.Contains(mainTag.Title)).Select(mainTag => mainTag).ToListAsync();
    }
}
