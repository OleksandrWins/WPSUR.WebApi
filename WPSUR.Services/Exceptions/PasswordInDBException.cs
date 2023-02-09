namespace WPSUR.Services.Exceptions
{
    public sealed class PasswordInDBException : Exception
    {
        public PasswordInDBException() : base("User with this email exists, please enter other email.")
        {

        }
    }
}
