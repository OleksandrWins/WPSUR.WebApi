using WPSUR.Services.Models.Account;

namespace WPSUR.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<User> LoginAsync(LoginUser model);
    }
}
