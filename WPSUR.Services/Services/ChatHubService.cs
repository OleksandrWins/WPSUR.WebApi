using Microsoft.AspNetCore.SignalR;
using WPSUR.Services.Interfaces;
using WPSUR.Services.Models;

namespace WPSUR.Services.Services
{
    public sealed class ChatHubService : IChatHubService
    {
        private readonly IHubContext<ChatHub> _chatHub;

        public ChatHubService(IHubContext<ChatHub> chatHub)
        {
            _chatHub = chatHub;
        }

        public async Task SendMessage(ChatMessage messageToSend)
        {
            await _chatHub.Clients.All.SendAsync("ReceiveMessage", messageToSend.Content, messageToSend.UserFrom, messageToSend.UserTo);
        }
    }
}
