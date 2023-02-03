using WPSUR.Repository.Entities;

namespace WPSUR.Repository.Interfaces
{
    public interface IUserRepository : IRepositotyBase<UserEntity>
    {
        public Task<(UserEntity, UserEntity)> GetSenderReceiver(Guid userFromId, Guid userToId);
    }
}
