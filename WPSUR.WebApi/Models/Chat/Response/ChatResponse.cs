using WPSUR.Services.Models.Chat;
using WPSUR.Services.Models.Messages;

namespace WPSUR.WebApi.Models.Chat.Response
{
    public sealed class ChatResponse
    {
        public ChatUser Receiver { get; set; }

        public ChatUser Sender { get; set; }    

        public ICollection<ChatMessage> Messages { get; set; }
    }
}
