namespace WPSUR.Services.Models
{
    public sealed class ChatMessage
    {
        public string Content { get; set; }

        public Guid UserFrom { get; set; }

        public Guid UserTo { get; set; }
    }
}
