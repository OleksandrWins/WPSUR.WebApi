namespace WPSUR.Services.Models.Comment.Request
{
    public class CreateCommentRequest
    {
        public string Content { get; set; }
        public Guid TargetPostId { get; set; }
        public Guid CreatorId { get;set; }
    }
}
