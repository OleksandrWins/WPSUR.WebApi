namespace WPSUR.WebApi.Models.Comment
{
    public sealed class CreateRequests
    {
        public string Content { get; set; }

        public Guid TargetPost { get; set; }

    }
}
