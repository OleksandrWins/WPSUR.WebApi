using WPSUR.Repository.Entities;
using WPSUR.Services.Models.Messages;

namespace WPSUR.Services.Models.Chat.Response
{
    public sealed class Chat
    {
        public ChatUser Sender { get; set; }

        public ChatUser Receiver { get; set; }

        public ICollection<ChatMessage> Messages { get; set; }
    }
}
