using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPSUR.Services.Exceptions.MessagesExceptions
{
    public class MessageDoesNotExistException : Exception
    {
        public MessageDoesNotExistException(string message) : base(message)
        {
        }

        public MessageDoesNotExistException(string message, Exception exception) : base(message, exception)
        {
        }
    }
}
