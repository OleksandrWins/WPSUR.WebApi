namespace WPSUR.Services.Models.Messages.Requests
{
    public sealed class MessageToUpdate
    {
        public Guid Id { get; set; }

        public string Content { get; set; }
    }
}
