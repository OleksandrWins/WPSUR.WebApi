using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> IsUserExistAsync(string email)
            => await GetByEmailImplementationAsync(email) is not null;

        public async Task CreateAsync(UserEntity user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<UserEntity> GetByEmailAsync(string email)
            => await GetByEmailImplementationAsync(email);

        private async Task<UserEntity> GetByEmailImplementationAsync(string email)
            => await _dbContext.Users.FirstOrDefaultAsync(user => EF.Functions.Like(user.Email, $"%{email}%"));
    }
}
