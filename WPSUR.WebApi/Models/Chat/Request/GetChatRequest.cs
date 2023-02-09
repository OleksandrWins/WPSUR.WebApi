namespace WPSUR.WebApi.Models.Chat.Request
{
    public sealed class GetChatRequest
    {
        public Guid UserFrom { get; set; }

        public Guid UserTo { get; set; }
    }
}
