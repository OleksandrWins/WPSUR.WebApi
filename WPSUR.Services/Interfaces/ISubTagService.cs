using WPSUR.Repository.Entities;

namespace WPSUR.Services.Interfaces
{
    public interface ISubTagService
    {
        public Task<SubTagEntity> GetOrCreateSubTagAsync(string subTagTitle);
    }
}
