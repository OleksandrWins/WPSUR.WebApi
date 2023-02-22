using WPSUR.Services.Models.Tags;

namespace WPSUR.Services.Models.Post
{
    public class PostModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public ICollection<CommentModel> Comments { get; set; }
        public MainTagModel MainTag { get; set; }
        public ICollection<SubTagModel> SubTags { get; set; }
        public Guid UserId { get; set; }
    }
}
