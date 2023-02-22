using WPSUR.Services.Models.Widgets;

namespace WPSUR.Services.Interfaces
{
    public interface IOccupantNumbersService
    {
        public Task<KilledRussiansModel> GetFreshNumbers();
    }
}
