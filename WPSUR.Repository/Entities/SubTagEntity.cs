using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WPSUR.Repository.Entities
{
    [Table("SubTag")]
    public class SubTagEntity : ManageableEntityBase
    {
        [MaxLength(50)]
        public string Title { get; set; }

        public ICollection<MainTagEntity> MainTags { get; set; }

        public ICollection<PostEntity> Posts { get; set; }
    }
}
