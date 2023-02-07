using WPSUR.Repository.Entities;

namespace WPSUR.Repository.Interfaces
{
    public interface IUserRepository : IRepositotyBase<UserEntity>
    {
        public Task<(UserEntity, UserEntity)> GetSenderReceiverAsync(Guid userFromId, Guid userToId);
    }
}
