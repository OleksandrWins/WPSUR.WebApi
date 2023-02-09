namespace WPSUR.Services.Exceptions
{
    public sealed class InvalidPasswordException : Exception
    {
        public InvalidPasswordException() : base("Invalid password, access denied. Try again.")
        {

        }
    }
}
