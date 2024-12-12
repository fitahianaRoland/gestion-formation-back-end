using GestionFormation.Models.classes;

namespace GestionFormation.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string emailBody,IFormFile file);
        Task SendEmailParticipatsAsync(EmailRequest emailRequest);

        Task SendLinklAsync(string toEmail, string subject, string emailBody);
        Task SendLinkEmailParticipatsAsync(int trainingId, int trainingSessionId, string trainingTheme, List<Participants> participants);

    }
}
