using WPSUR.Repository.Entities;

namespace WPSUR.Services.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(IReadOnlyCollection<string> receiverEmails, string receiverContent);
    }
}
