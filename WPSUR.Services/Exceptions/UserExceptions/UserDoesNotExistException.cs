namespace WPSUR.Services.Exceptions.UserExceptions
{
    public class UserDoesNotExistException : Exception
    {
        public UserDoesNotExistException(string message) : base(message)
        {
        }

        public UserDoesNotExistException(string message, Exception exception) : base(message, exception)
        {
        }
    }
}
