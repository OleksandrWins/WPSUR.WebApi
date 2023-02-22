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

        public async Task<MessageEntity> GetMessage(Guid id)
        {
            MessageEntity message = await _dbContext.Messages.Include(message => message.CreatedBy)
                                                             .Include(message => message.UserFrom)
                                                             .Include(message => message.UserTo)
                                                             .FirstOrDefaultAsync(message => message.Id == id) ?? throw new NullReferenceException(nameof(message));

            return message;
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
                                        .Include(message => message.UserTo)
                                        .ToListAsync();

        public async Task UpdateAsync(MessageEntity message, string Content, DateTime updatedDate)
        {
            _dbContext.Update(message);

            message.UpdatedBy = message.UserFrom;
            message.Content = Content;
            message.UpdatedData = updatedDate;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<ICollection<MessageEntity>> GetChatCollectionAsync(Guid senderId, Guid receiverId)
            => await _dbContext.Messages.Where(message => message.DeletedBy == null && ((message.UserFrom.Id.Equals(senderId) && message.UserTo.Id.Equals(receiverId)) 
                                        || (message.UserFrom.Id.Equals(receiverId) && message.UserTo.Id.Equals(senderId))))
                                        .Select(message => message)
                                        .Include(message => message.UserTo)
                                        .Include(message => message.UserFrom)
                                        .ToListAsync();

        public async Task<ICollection<MessageEntity>> GetUserMessagesAsync(Guid senderId) 
            => await _dbContext.Messages.Where(message => senderId == message.UserTo.Id || senderId == message.UserFrom.Id).Include(message => message.UserTo).Include(message => message.UserFrom).ToListAsync();
    }
}
