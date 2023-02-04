using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WPSUR.Repository.Entities
{
    [Table("Post")]
    public sealed class PostEntity : ManageableEntityBase
    {
        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Body { get; set; }

        public MainTagEntity MainTag { get; set; }

        public ICollection<SubTagEntity> SubTags { get; set; }
    }
}
