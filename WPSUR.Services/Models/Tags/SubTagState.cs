using WPSUR.Services.Models.Post;

namespace WPSUR.Services.Models.Tags
{
    public class SubTagState
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public ICollection<MainTagState> MainTags { get; set; }
        public ICollection<PostState> Posts { get; set; }
    }
}
