using WPSUR.Services.Models.Messages;

namespace WPSUR.WebApi.Models.Chat.Response
{
    public sealed class ChatResponse
    {
        public Guid UserFrom { get; set; }

        public Guid UserTo { get; set; }

        public ICollection<ChatMessage> Messages { get; set; }
    }
}
