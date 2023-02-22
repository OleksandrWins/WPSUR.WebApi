namespace WPSUR.WebApi.Models.Tags
{
    public sealed class SearchByMainAndSubTagsRequest
    {
        public Guid MainTagId { get; set; }
        public Guid SubTagId { get; set; }
    }
}
