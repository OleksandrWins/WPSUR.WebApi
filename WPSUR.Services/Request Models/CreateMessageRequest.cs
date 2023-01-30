
using WPSUR.Repository.Entities;

namespace WPSUR.Services.Request_Models
{
    public class CreateMessageRequest
    {
        public string Content { get; set; }
        public UserEntity UserFrom { get; set; }
        public UserEntity UserTo { get; set; }
    }
}
