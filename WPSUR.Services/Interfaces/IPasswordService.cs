using WPSUR.Services.Models.Account;

namespace WPSUR.Services.Interfaces
{
    public interface IPasswordHashService
    {
        public string HashPassword(string password);

        public bool ValidatePassword(string providedPassword, string hashedPassword);
    }
}
