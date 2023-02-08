namespace WPSUR.WebApi.Models.Account.Request
{
    public sealed class LoginRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
