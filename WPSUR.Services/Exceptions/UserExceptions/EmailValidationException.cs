namespace WPSUR.Services.Exceptions.UserExceptions
{
    public class EmailValidationException : Exception
    {
        public EmailValidationException() : base("Email is invalid, please try again.")
        { 
        }
    }
}
