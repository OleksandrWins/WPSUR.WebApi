namespace WPSUR.WebApi.Models.Message.Request
{
    public sealed class UpdateMessageRequest
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
    }
}
