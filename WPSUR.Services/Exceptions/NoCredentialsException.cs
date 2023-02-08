namespace WPSUR.Services.Exceptions
{
    public sealed class NoCredentialsException : Exception
    {
        public NoCredentialsException() : base("Please fill all fields in form, then try again.")
        {
            
        }
    }
}
