using System.ComponentModel.DataAnnotations.Schema;

namespace WPSUR.Repository.Entities
{
    [Table("Comment")]
    public sealed class CommentEntity : ManageableEntityBase
    {
        public string Content { get; set; }

        public ICollection<UserEntity> Likes { get; set; }

        public PostEntity TargetPost { get; set; }
    }
}
