using WPSUR.Repository.Entities;
using WPSUR.Repository.Interfaces;

namespace WPSUR.Repository.Repositories
{
    public sealed class UserRepository : RepositoryBase<UserEntity>, IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task <bool>  IsUserExistAsync(string email)
            => _dbContext.Users.Any(user => user.Email == email);

        public async Task CreateAsync(UserEntity user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }
        
        public async Task<UserEntity> GetByEmailAsync(string email) 
            => _dbContext.Users.FirstOrDefault(user => user.Email.Equals(email, StringComparison.OrdinalIgnoreCase)); 
    }
}
