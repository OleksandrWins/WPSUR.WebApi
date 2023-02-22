namespace WPSUR.WebApi.Models.Chat.Response
{
    public class ChatReceiverResponse
    {
        public Guid ReceiverId { get; set; }

        public string ReceiverFirstName { get; set; }
        
        public string ReceiverLastName { get; set; }

        public string ReceiverEmail { get; set; }
    }
}
