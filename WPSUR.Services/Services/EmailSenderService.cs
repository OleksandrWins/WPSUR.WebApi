using System.Net;
using System.Net.Mail;
using System.Text;
using WPSUR.Services.Interfaces;

namespace WPSUR.Services.Services
{
    public sealed class EmailSenderService : IEmailSender
    {
        public async Task SendEmailAsync(IReadOnlyCollection<string> receiverEmails, string receiverContent)
        {
            string emailSender = "asptestapplication@gmail.com";

            using SmtpClient client = new("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(emailSender, "iatxvmubxmzkistj"),
            };

            foreach (var receiverEmail in receiverEmails)
            {
                using MailMessage message = new(emailSender, receiverEmail)
                {
                    Subject = "Everybody Info emergency broadcast",
                    Body = receiverContent,
                    BodyEncoding = Encoding.UTF8,
                };

                await client.SendMailAsync(message);
            }
        }
    }
}
