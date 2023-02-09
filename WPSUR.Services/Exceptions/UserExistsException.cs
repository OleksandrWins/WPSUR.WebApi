namespace WPSUR.Services.Exceptions
{
    public class UserExistsException : Exception
    {
        public UserExistsException() : base("User with this email exists, please log in or use other email.")
        {

        }
    }
}
