using System.ComponentModel.DataAnnotations.Schema;
using WPSUR.Repository.EntityEnums;

namespace WPSUR.Repository.Entities
{
    [Table("User")]
    public sealed class UserEntity : EntityBase
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public UserEntityRole Role { get; set; }

        public string PasswordHash { get; set; }

        public string EmergencyContent { get; set; }

        public string EmergencyList { get; set; }
    }
}
