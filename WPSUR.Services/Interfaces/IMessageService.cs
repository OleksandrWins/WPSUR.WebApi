using WPSUR.Services.Models.Messages;

namespace WPSUR.Services.Interfaces
{
    public interface IMessageService
    {
        public Task CreateAsync(ChatMessage message);

        public Task DeleteMessagesAsync(ICollection<Guid> Ids);
    }
}
