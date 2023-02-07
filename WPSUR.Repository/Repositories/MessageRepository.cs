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
    }
}
