using WPSUR.Services.Models.Account;

namespace WPSUR.Services.Interfaces
{
    public interface ITokenService
    {
        public string GetToken(User user);
    }
}
