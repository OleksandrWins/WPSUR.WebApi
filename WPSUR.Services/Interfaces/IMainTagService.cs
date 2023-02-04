using WPSUR.Repository.Entities;
using WPSUR.Services.Models.Post;

namespace WPSUR.Services.Interfaces
{
    public interface IMainTagService
    {
        public Task<MainTagEntity> CreateMainTag(PostModel postModel);
    }
}
