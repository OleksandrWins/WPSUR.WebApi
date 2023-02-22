using WPSUR.Repository.Entities;
using WPSUR.Repository.Interfaces;
using WPSUR.Services.Interfaces;
using WPSUR.Services.Models.Emergency;

namespace WPSUR.Services.Services
{
    public sealed class EmergencyService : IEmergencyService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailSender _emailSender;

        public EmergencyService(IUserRepository userRepository, IEmailSender sender)
        {
            _userRepository = userRepository;
            _emailSender = sender;
        }

        public async Task<EmergencyInfo> GetEmergencyInfoAsync(Guid userId)
        {
            UserEntity user = await _userRepository.GetByIdAsync(userId);
            return new EmergencyInfo()
            {
                EmergencyList = user.EmergencyList,
                EmergencyContent = user.EmergencyContent,
            };
        }

        public async Task SetEmergencyInfoAsync(Guid userId, EmergencyInfo info)
        {
            UserEntity user = await _userRepository.GetByIdAsync(userId);
            user.EmergencyContent = info.EmergencyContent;
            user.EmergencyList = info.EmergencyList;
            await _userRepository.UpdateAsync(user);
        }

        public async Task EmergencyCallAsync(Guid userId)
        {
            UserEntity user = await _userRepository.GetByIdAsync(userId);

            IReadOnlyCollection<string> receiverEmails = user.EmergencyList
                .Split(";")
                .Where(receiverEmail => !string.IsNullOrEmpty(receiverEmail))
                .ToList();
            await _emailSender.SendEmailAsync(receiverEmails, user.EmergencyContent);
        }
    }
}
