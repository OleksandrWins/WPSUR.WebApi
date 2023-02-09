namespace WPSUR.Services.Models.Tags
{
    public class SubTag
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public ICollection<string> MainTags { get; set; }
        public ICollection<string> Posts { get; set; }
    }
}
