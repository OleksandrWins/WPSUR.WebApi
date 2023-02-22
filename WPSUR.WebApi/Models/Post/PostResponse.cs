using WPSUR.Services.Models.Tags;

namespace WPSUR.WebApi.Models.Post
{
    public sealed class PostResponse
    {
        public Guid Id { get; set; }    
        public ICollection<CommentResponse>? Comments { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public Guid CreatedBy { get; set; }
        public MainTagModel MainTag { get; set; }
        public ICollection<SubTagModel> SubTags { get; set; }
    }
}

