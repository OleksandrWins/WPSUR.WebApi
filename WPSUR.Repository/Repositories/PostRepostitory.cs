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

        public async Task CreateAsync(PostEntity post)
        {
            await _dbContext.Posts.AddAsync(post);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<ICollection<PostEntity>> GetByMainTagAndSubTagIdsAsync(Guid MainTagId, Guid SubTagId)
            => await _dbContext.Posts.Where(post => post.MainTag.Id == MainTagId && post.SubTags.Any(subtag => subtag.Id == SubTagId)).ToListAsync();

        public async Task<ICollection<PostEntity>> GetByTitleAsync(string title)
            => await _dbContext.Posts.Where(post => post.Title.Contains(title)).ToListAsync();

        public async Task<ICollection<PostEntity>> GetPostsAsync()
            => await _dbContext.Posts.Include(post => post.CreatedBy).Include(post => post.Comments).ThenInclude(comment => comment.CreatedBy).Include(post => post.MainTag).Include(post => post.SubTags).Take(10).ToListAsync();
        
        //public async Task<ICollection<SubTagEntity>> GetSubTagsAsync(Guid mainTagId)
        //    => await _dbContext.SubTags.Where(s => s.MainTags.Any(mainTag => mainTag.Id == mainTagId)).ToListAsync();

    }
}

