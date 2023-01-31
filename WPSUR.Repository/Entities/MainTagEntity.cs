using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WPSUR.Repository.Entities
{
    [Table("MainTag")]
    public class MainTagEntity: ManageableEntityBase
    {
        [MaxLength(50)]
        public string Title { get; set; }

        public ICollection<SubTagEntity> SubTags { get; set; }

        public ICollection<PostEntity> Posts { get; set; }
    }
}
