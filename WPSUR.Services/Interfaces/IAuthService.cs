using WPSUR.Services.Models.Account;

namespace WPSUR.Services.Interfaces
{
    public interface IAuthService
    {
        public User Login(LoginUser model);
    }
}
