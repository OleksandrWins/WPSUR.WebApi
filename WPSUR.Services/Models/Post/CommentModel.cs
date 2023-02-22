using WPSUR.Services.Models.Account;

namespace WPSUR.Services.Models.Post
{
    public class CommentModel
    {
        public Guid Id { get; set; }
        public ICollection<Guid> Likes { get; set; }
        public string Content { get; set; }
        public UserModel CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}