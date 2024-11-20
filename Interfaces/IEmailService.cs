using GestionFormation.Models.classes;

namespace GestionFormation.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string emailBody,IFormFile file);
        Task SendEmailParticipatsAsync(EmailRequest emailRequest);
    }
}
