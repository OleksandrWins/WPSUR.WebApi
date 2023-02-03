using WPSUR.Repository.Entities;
using WPSUR.Services.Models;

namespace WPSUR.Services.Interfaces
{
    public interface IMessageService
    {
        public Task CreateMessageAsync(ChatMessage message);
    }
}
