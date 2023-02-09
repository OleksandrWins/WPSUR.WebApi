using Microsoft.EntityFrameworkCore;
using System.Reflection;
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

                UserEntity sender = new();
                UserEntity receiver = new();

                if (userFromId.Equals(senderReceiverCollection[0].Id))
                {
                    sender = senderReceiverCollection[0];
                    receiver = senderReceiverCollection[1];
                }

                if (userFromId.Equals(senderReceiverCollection[1].Id))
                {
                    sender = senderReceiverCollection[1];
                    receiver = senderReceiverCollection[0];
                }

                (UserEntity sender, UserEntity receiver) result = (sender, receiver);

                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}
