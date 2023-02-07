﻿using WPSUR.Repository.Entities;

namespace WPSUR.Repository.Interfaces
{
    public interface IMessageRepository : IManageableRepositoryBase<MessageEntity>
    {
        public Task CreateAsync(MessageEntity message);

        public Task<ICollection<MessageEntity>> GetMessagesCollectionAsync(ICollection<Guid> messagesIds);

        public Task DeleteCollectionAsync(ICollection<MessageEntity> message, UserEntity user);
    }
}
