using WPSUR.Services.Models.Messages;
using WPSUR.Services.Models.Messages.Requests;

namespace WPSUR.Services.Interfaces
{
    public interface IMessageService
    {
        public Task<Guid> CreateAsync(ChatMessage message);

        public Task DeleteMessagesAsync(ICollection<Guid> Ids);

        public Task UpdateAsync(MessageToUpdate messageToUpdate);
    }
}
