namespace WPSUR.Services.Models.Tags
{
    public class MainTag
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public ICollection<string> SubTags { get; set; }
        public ICollection<string> Posts { get; set; }
    }
}
