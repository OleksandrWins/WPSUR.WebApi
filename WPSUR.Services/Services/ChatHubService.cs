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
            await _chatHub.Clients.All.SendAsync("ReceiveMessage", messageToSend);
        }

        public async Task NotifyMessageDeletion(MessageDeletionNotification deletionNotification)
        {
            await _chatHub.Clients.All.SendAsync("DeleteMessage", deletionNotification);
        }

        public async Task NotifyMessageUpdate(MessageUpdateNotification udpateNotification)
        {
            await _chatHub.Clients.All.SendAsync("UpdateMessage", udpateNotification);
        }
    }
}
