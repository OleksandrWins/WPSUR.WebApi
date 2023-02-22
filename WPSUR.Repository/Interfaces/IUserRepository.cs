using WPSUR.Repository.Entities;

namespace WPSUR.Repository.Interfaces
{
    public interface IUserRepository : IRepositotyBase<UserEntity>
    {
        public Task<UserEntity> GetByEmailAsync(string email);

        public Task<(UserEntity, UserEntity)> GetSenderReceiverAsync(Guid userFromId, Guid userToId);

        public Task<bool> IsUserExistAsync(string email);

        public Task CreateAsync(UserEntity user);

        public Task UpdateAsync(UserEntity user);
        
    }
}
