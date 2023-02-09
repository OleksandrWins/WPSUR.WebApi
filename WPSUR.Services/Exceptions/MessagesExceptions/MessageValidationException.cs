namespace WPSUR.Services.Exceptions.MessagesExceptions
{
    public class MessageValidationException : Exception
    {
        public MessageValidationException(string message) : base(message)
        {
        }

        public MessageValidationException(string message, Exception exception) : base(message, exception)
        {
        }
    }
}
