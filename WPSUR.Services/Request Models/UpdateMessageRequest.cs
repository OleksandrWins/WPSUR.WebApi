using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPSUR.Services.Request_Models
{
    public class UpdateMessageRequest
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
    }
}
