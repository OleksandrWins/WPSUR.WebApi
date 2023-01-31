using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WPSUR.Repository.Entities
{
    [Table("Message")]
    public sealed class MessageEntity : ManageableEntityBase
    {
        public UserEntity UserFrom { get; set; }

        public UserEntity UserTo { get; set; }

        [MaxLength(1000)]
        public string Content { get; set; }
    }
}
