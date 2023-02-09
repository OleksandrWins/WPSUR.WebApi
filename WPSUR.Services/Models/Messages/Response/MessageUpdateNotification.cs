using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPSUR.Services.Models.Messages.Response
{
    public sealed class MessageUpdateNotification
    {
        public Guid ReceiverId { get; set; }

        public Guid SenderId { get; set; }

        public Guid MessageId { get; set; }

        public string Content { get; set; } 
    }
}
