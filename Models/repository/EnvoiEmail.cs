using System.Net.Mail;
using System.Net;

namespace GestionFormation.Models.repository
{
    public class EnvoiEmail
    {
        public async Task<bool> SendEmailAsync(string toEmail, string subject, string body, List<string>? ccEmails = null)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("rmirah26@gmail.com", "gtaa zphu oiml tqip"),
                    EnableSsl = true
                };

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("rmirah26@gmail.com"),
                    Subject = subject,                            
                    Body = body,                                  
                    IsBodyHtml = true                             
                };

                mailMessage.To.Add(toEmail);

                if (ccEmails != null && ccEmails.Count > 0)
                {
                    foreach (var ccEmail in ccEmails)
                    {
                        mailMessage.CC.Add(ccEmail); 
                    }
                }

                await client.SendMailAsync(mailMessage);

                return true;
            }
            catch (SmtpException ex)
            {
                // Gérer l'erreur si l'envoi échoue
                Console.WriteLine($"Erreur lors de l'envoi de l'email : {ex.Message}");
                return false;
            }
        }

    }
}
