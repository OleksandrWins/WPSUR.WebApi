using WPSUR.Repository.Entities;
using WPSUR.Repository.Interfaces;
using WPSUR.Services.Enums;
using WPSUR.Services.Exceptions;
using WPSUR.Services.Interfaces;
using WPSUR.Services.Models.Account;

namespace WPSUR.Services.Services
{
    public sealed class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHashService _passwordHashService;

        public AuthService(IUserRepository userRepository, IPasswordHashService passwordHashService)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _passwordHashService = passwordHashService ?? throw new ArgumentNullException(nameof(passwordHashService));
        }

        public async Task<User> LoginAsync(LoginUser userModel)
        {
            userModel.Email = userModel.Email.ToLower();
            UserEntity existingUser = await _userRepository.GetByEmailAsync(userModel.Email);
            if (existingUser == null) 
            {
                throw new ArgumentNullException(nameof(userModel));
            }

            bool validate = _passwordHashService.ValidatePassword(userModel.Password, existingUser.PasswordHash);

            if (!validate) 
            {
                throw new InvalidPasswordException();
        }

            return new User()
        {
                FirstName = existingUser.FirstName,
                Email = existingUser.Email,
                LastName = existingUser.LastName,
                Role = (UserRole)existingUser.Role,
                Id = existingUser.Id,
            };
        }
    }
}
