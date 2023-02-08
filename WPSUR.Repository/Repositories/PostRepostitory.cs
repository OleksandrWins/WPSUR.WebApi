using WPSUR.Repository.Entities;
using WPSUR.Repository.Interfaces;

namespace WPSUR.Repository.Repositories
{
    public sealed class PostRepostitory : ManageableRepositoryBase<PostEntity>, IPostRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PostRepostitory(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task CreateAsync(PostEntity post)
        {
            await _dbContext.Posts.AddAsync(post);

            await _dbContext.SaveChangesAsync();
        }
    }
}
