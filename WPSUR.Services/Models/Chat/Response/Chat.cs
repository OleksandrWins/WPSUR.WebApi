using WPSUR.Repository.Entities;
using WPSUR.Services.Models.Messages;

namespace WPSUR.Services.Models.Chat.Response
{
    public sealed class Chat
    {
        public Guid UserFrom { get; set; }

        public Guid UserTo { get; set; }

        public ICollection<ChatMessage> Messages { get; set; }
    }
}
