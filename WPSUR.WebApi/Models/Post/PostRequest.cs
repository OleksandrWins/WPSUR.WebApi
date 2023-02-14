namespace WPSUR.WebApi.Models.Post
{
    public sealed class PostRequest
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string MainTag { get; set; }
        public ICollection<string> SubTags { get; set; }
        public Guid UserId { get; set; }
    }
}
