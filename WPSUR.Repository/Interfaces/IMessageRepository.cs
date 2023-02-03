using WPSUR.Repository.Entities;

namespace WPSUR.Repository.Interfaces
{
    public interface IMessageRepository : IManageableRepositoryBase<MessageEntity>
    {
        public Task CreateMessageAsync(MessageEntity message);
    }
}
