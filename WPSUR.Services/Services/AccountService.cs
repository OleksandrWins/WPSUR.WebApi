using WPSUR.Repository.Entities;
using WPSUR.Repository.EntityEnums;
using WPSUR.Repository.Interfaces;
using WPSUR.Services.Exceptions;
using WPSUR.Services.Interfaces;
using WPSUR.Services.Models.Account;

namespace WPSUR.Services.Services
{
    public sealed class AccountService : ServiceBase, IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHashService _passwordHashService;

        public AccountService(IUserRepository userRepository, IPasswordHashService passwordHashService)
        {
            _userRepository = userRepository;
            _passwordHashService = passwordHashService;
        }

        public async Task<User> RegisterAsync(RegisterUser userModel)
        {
            if (IsNullOrWhiteSpace(userModel.Email) || IsNullOrWhiteSpace(userModel.FirstName) || IsNullOrWhiteSpace(userModel.Password))
            {
                throw new NoCredentialsException();
            }

            if (userModel.Password.Length < 8 || userModel.Password.Length > 14)
            {
                throw new PasswordStrengthException();
            }

            bool isUserExist = await _userRepository.IsUserExistAsync(userModel.Email);
            if (isUserExist)
            {
                throw new UserExistsException();
            }

            string passwordHash = _passwordHashService.HashPassword(userModel.Password);

            UserEntity userToDb = new()
            {
                PasswordHash = passwordHash,
                FirstName = userModel.FirstName,
                Email = userModel.Email,
                LastName = userModel.LastName,
                Id = Guid.NewGuid(),
                Role = UserEntityRole.User,
            };

            await _userRepository.CreateAsync(userToDb);

            return new User()
            {
                FirstName = userToDb.FirstName,
                Email = userToDb.Email,
                LastName = userToDb.LastName,
                Id = userToDb.Id,
            };
        }
    }
}
