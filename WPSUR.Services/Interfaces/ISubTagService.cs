using WPSUR.Repository.Entities;

namespace WPSUR.Services.Interfaces
{
    public interface ISubTagService
    {
        public Task<SubTagEntity> CreateSubTag(string subTagTitle);
    }
}
