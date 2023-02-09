using WPSUR.Services.Models.Chat.Request;
using WPSUR.Services.Models.Chat.Response;

namespace WPSUR.Services.Interfaces
{
    public interface IChatService
    {
        public Task<Chat> GetChat(GetChatServiceRequest getChatRequest);

        public Task<ICollection<Guid>> GetInterlocutors(Guid senderId);
    }
}
