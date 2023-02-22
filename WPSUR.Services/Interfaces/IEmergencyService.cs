using WPSUR.Services.Models.Emergency;

namespace WPSUR.Services.Interfaces
{
    public interface IEmergencyService
    {
        public Task<EmergencyInfo> GetEmergencyInfoAsync(Guid userId);
        public Task SetEmergencyInfoAsync(Guid userId, EmergencyInfo info);
        public Task EmergencyCallAsync(Guid userId);
    }
}
