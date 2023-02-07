using WPSUR.Services.Models.Messages;
using WPSUR.Services.Models.Messages.Response;

namespace WPSUR.Services.Interfaces
{
    public interface IChatHubService
    {
        public Task SendMessageAsync(ChatMessage messageToSend);

        public Task DeleteMessageAsync(MessageDeletionNotification deletionNotification);
    }
}
