using Microsoft.EntityFrameworkCore;
using WPSUR.Repository.Entities;
using WPSUR.Repository.Interfaces;

namespace WPSUR.Repository.Repositories
{
    public sealed class MessageRepository : ManageableRepositoryBase<MessageEntity>, IMessageRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public MessageRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task CreateAsync(MessageEntity message)
        {
            await _dbContext.Messages.AddAsync(message);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCollectionAsync(ICollection<MessageEntity> messages, UserEntity user)
        {
            _dbContext.UpdateRange(messages);

            foreach (MessageEntity message in messages)
            {
                message.DeletedBy = user;
                message.DeletedDate = DateTime.UtcNow;
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<ICollection<MessageEntity>> GetMessagesCollectionAsync(ICollection<Guid> messagesIds) 
            => await _dbContext.Messages.Where(message => messagesIds.Contains(message.Id))
                                        .Select(message => message)
                                        .Include(message => message.UserFrom)
                                        .ToListAsync();

        public async Task UpdateAsync(MessageEntity message, string Content)
        {
            _dbContext.Update(message);

            message.UpdatedBy = message.UserFrom;
            message.Content = Content;
            message.UpdatedData = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<ICollection<MessageEntity>> GetChatCollectionAsync(Guid senderId, Guid receiverId)
            => await _dbContext.Messages.Where(message => (message.UserFrom.Id.Equals(senderId) && message.UserTo.Id.Equals(receiverId)) 
                                        || (message.UserFrom.Id.Equals(receiverId) && message.UserTo.Id.Equals(senderId)))
                                        .Select(message => message)
                                        .Include(message => message.UserTo)
                                        .Include(message => message.UserFrom)
                                        .ToListAsync();

        public async Task<ICollection<Guid>> GetUserChats(Guid senderId)
        {
            var groupedMessagesbySender = await _dbContext.Messages.Where(message => message.UserFrom.Id == senderId)
                                                                   .Select(message => new { sender = message.UserFrom.Id, receiver = message.UserTo.Id })
                                                                   .GroupBy(messages => messages.sender)
                                                                   .ToListAsync();
            
            ICollection<Guid> chatCollection = groupedMessagesbySender[0].DistinctBy(message => message.receiver).Select(message => message.receiver).ToList();

            return chatCollection;
        }
    }
}
