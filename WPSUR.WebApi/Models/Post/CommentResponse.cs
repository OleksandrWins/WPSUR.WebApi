namespace WPSUR.WebApi.Models.Post
{
    public class CommentResponse
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public UserResponse CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
