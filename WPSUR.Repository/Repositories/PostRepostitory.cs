
using WPSUR.Repository.Entities;
using WPSUR.Repository.Interfaces;

namespace WPSUR.Repository.Repositories
{
    public class PostRepostitory : ManageableRepositoryBase<PostEntity> 
    {
        private readonly ApplicationDbContext _dbContext;
        public PostRepostitory(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
    }
}
