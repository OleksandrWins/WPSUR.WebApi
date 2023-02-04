namespace WPSUR.WebApi.Models.Post
{
    public class PostRequest
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string MainTag { get; set; }
        public ICollection<string> SubTags { get; set; }
    }
}
