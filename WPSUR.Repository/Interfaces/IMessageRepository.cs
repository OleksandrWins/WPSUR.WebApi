using WPSUR.Repository.Entities;

namespace WPSUR.Repository.Interfaces
{
    public interface IMessageRepository : IManageableRepositoryBase<MessageEntity>
    {
        public Task CreateAsync(MessageEntity message);

        public Task<ICollection<MessageEntity>> GetMessagesCollectionAsync(ICollection<Guid> messagesIds);

        public Task DeleteCollectionAsync(ICollection<MessageEntity> message, UserEntity user);

        public Task UpdateAsync(MessageEntity message, string Content, DateTime updatedDate);

        public Task<ICollection<MessageEntity>> GetChatCollectionAsync(Guid senderId, Guid receiverId);

        public Task<ICollection<MessageEntity>> GetUserMessagesAsync(Guid senderId);
        Task<MessageEntity> GetMessage(Guid id);
    }
}
