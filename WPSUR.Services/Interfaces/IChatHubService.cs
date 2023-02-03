using WPSUR.Services.Models;

namespace WPSUR.Services.Interfaces
{
    public interface IChatHubService
    {
        public Task SendMessage(ChatMessage messageToSend);
    }
}
