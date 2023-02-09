using WPSUR.Services.Models.Account;

namespace WPSUR.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<User> RegisterAsync(RegisterUser user);
    }
}
