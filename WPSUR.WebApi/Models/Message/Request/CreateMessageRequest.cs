namespace WPSUR.WebApi.Models.Message.Request
{
    public sealed class CreateMessageRequest
    {
        public string Content { get; set; }
        public Guid UserFrom { get; set; }
        public Guid UserTo { get; set; }
    }
}
