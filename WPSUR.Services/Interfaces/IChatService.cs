using WPSUR.Services.Models.Chat.Request;
using WPSUR.Services.Models.Chat.Response;

namespace WPSUR.Services.Interfaces
{
    public interface IChatService
    {
        public Task<ICollection<ChatsResponse>> FindChats(string receiverEmail, Guid userId);
        public Task<Chat> GetChat(GetChatServiceRequest getChatRequest);

        public Task<ICollection<ChatsResponse>> GetInterlocutors(Guid senderId);
    }
}
