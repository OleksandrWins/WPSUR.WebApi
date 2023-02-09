namespace WPSUR.Services.Models.Messages
{
    public sealed class ChatMessage
    {
        public Guid Id { get; set; }

        public string Content { get; set; }

        public Guid UserFrom { get; set; }

        public Guid UserTo { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set;}
    }
}
