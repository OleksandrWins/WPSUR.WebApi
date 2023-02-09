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

        public async Task<(UserEntity, UserEntity)> GetSenderReceiverAsync(Guid userFromId, Guid userToId)
        {
            try
            {
                List<UserEntity> senderReceiverCollection = await _dbContext.Users.Where(user => user.Id == userFromId || user.Id == userToId).Select(user => user).ToListAsync();

                if (senderReceiverCollection.Count != 2)
                {
                    throw new ArgumentException("Invalid response occured from database while getting data from it.");
                }

                (UserEntity, UserEntity) result = (senderReceiverCollection[0], senderReceiverCollection[1]);

                return result;
            }
            catch
            {
                throw;
            }
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
