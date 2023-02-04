namespace WPSUR.Services.Models.Post
{
    public class PostModel
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string MainTag { get; set; }
        public ICollection<string> SubTags { get; set; }
    }
}
