namespace WPSUR.Services.Models.Messages.Response
{
    public sealed class MessageUpdateNotification
    {
        public Guid ReceiverId { get; set; }

        public Guid SenderId { get; set; }

        public Guid MessageId { get; set; }

        public string Content { get; set; } 

        public DateTime UpdatedDate { get; set; }
    }
}
