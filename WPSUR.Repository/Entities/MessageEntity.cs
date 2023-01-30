using System.ComponentModel.DataAnnotations;

namespace WPSUR.Repository.Entities
{
    public sealed class MessageEntity : ManageableEntityBase
    {
        public UserEntity UserFrom { get; set; }

        public UserEntity UserTo { get; set; }

        [MaxLength(1000)]
        public string Content { get; set; }
    }
}
