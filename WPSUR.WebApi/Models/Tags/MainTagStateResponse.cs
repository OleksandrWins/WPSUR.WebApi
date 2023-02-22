using WPSUR.Services.Models.Post;
using WPSUR.Services.Models.Tags;
using WPSUR.WebApi.Models.Post;

namespace WPSUR.WebApi.Models.Tags
{
    public class MainTagStateResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public ICollection<SubTagStateResponse> SubTags { get; set; }
        public ICollection<PostStateResponse> Posts { get; set; }
    }
}
