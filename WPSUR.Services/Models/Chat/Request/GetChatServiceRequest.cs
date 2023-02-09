namespace WPSUR.Services.Models.Chat.Request
{
    public sealed class GetChatServiceRequest
    {
        public Guid UserFromId { get; set; }

        public Guid UserToId { get; set; }
    }
}
