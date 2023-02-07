using Microsoft.AspNetCore.SignalR;
using WPSUR.Services.Interfaces;
using WPSUR.Services.Models.Messages;
using WPSUR.Services.Models.Messages.Response;

namespace WPSUR.Services.Services
{
    public sealed class ChatHubService : IChatHubService
    {
        private readonly IHubContext<ChatHub> _chatHub;

        public ChatHubService(IHubContext<ChatHub> chatHub)
        {
            _chatHub = chatHub ?? throw new ArgumentNullException(nameof(chatHub));
        }

        public async Task SendMessageAsync(ChatMessage messageToSend)
        {
            await _chatHub.Clients.All.SendAsync("ReceiveMessage", messageToSend.Content, messageToSend.UserFrom, messageToSend.UserTo);
        }

        public async Task DeleteMessageAsync(MessageDeletionNotification deletionNotification)
        {
            await _chatHub.Clients.All.SendAsync("DeleteMessage", deletionNotification.ReceiverId, deletionNotification.SenderId, deletionNotification.MessageIds);
        }
    }
}
