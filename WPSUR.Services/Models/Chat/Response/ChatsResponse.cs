namespace WPSUR.Services.Models.Chat.Response
{
    public sealed class ChatsResponse
    {
        public Guid UserId { get; set; }

        public string UserToFirstName { get; set; }

        public string UserToLastName { get; set; }

        public string UserEmail { get; set; }   
    }
}
