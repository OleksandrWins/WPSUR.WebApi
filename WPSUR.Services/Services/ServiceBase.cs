namespace WPSUR.Services.Services
{
    public abstract class ServiceBase
    {
        protected bool IsNullOrWhiteSpace(string @string)
            => string.IsNullOrWhiteSpace(@string);
    }
}
