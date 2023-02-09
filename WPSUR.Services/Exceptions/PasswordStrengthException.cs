namespace WPSUR.Services.Exceptions
{
    public class PasswordStrengthException : Exception
    {
        public PasswordStrengthException() : base("Password should contain 8 to 14 symbols.")
        {

        }
    }
}
