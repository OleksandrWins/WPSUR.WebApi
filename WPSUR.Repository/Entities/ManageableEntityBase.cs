using System.ComponentModel.DataAnnotations;

namespace WPSUR.Repository.Entities
{
    public abstract class ManageableEntityBase : EntityBase
    {
        public DateTime? DeletedDate { get; set; }

        public UserEntity DeletedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        public UserEntity CreatedBy { get; set; }

        public DateTime? UpdatedData { get; set; }

        public UserEntity UpdatedBy { get; set; }
    }
}
