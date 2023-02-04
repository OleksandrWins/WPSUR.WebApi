using Microsoft.EntityFrameworkCore;
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
        public async Task SaveNewPost(PostEntity post)
        {
            try
            {
                await _dbContext.Posts.AddAsync(post);

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<PostEntity> GetPostByTitleAsync(string title)
        {
            try
            {
                PostEntity post = await _dbContext.Set<PostEntity>().FirstOrDefaultAsync(x => x.Title == title);

                return post ?? throw new NullReferenceException(nameof(post));
            }
            catch
            {
                throw;
            }
        }
        public async Task<PostEntity> GetPostByBodyAsync(string body)
        {
            try
            {
                PostEntity post = await _dbContext.Set<PostEntity>().FirstOrDefaultAsync(x => x.Body == body);

                return post ?? throw new NullReferenceException(nameof(post));
            }
            catch
            {
                throw;
            }
        }
    }
}
