namespace WPSUR.Services.Models.Messages.Response
{
    public sealed class MessageDeletionNotification
    {
        public Guid ReceiverId { get; set; }

        public Guid SenderId { get; set; }

        public ICollection<Guid> MessageIds { get; set; }
    }
}
