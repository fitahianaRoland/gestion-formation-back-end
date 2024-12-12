using GestionFormation.Interfaces;
using System.Net.Mail;
using System.Net;
using GestionFormation.Models.classes;
using System.Text.Json;
using System.IO;
using GestionFormation.Models.repository;

namespace GestionFormation.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly TrainingRepository _trainingRepository;
        private readonly GenerateJwtTokenForLink _generateJwtTokenForLink;

        public EmailService(IConfiguration configuration,TrainingRepository trainingRepository, GenerateJwtTokenForLink generateJwtTokenForLink)
        {
            _configuration = configuration;
            _trainingRepository = trainingRepository;
            _generateJwtTokenForLink = generateJwtTokenForLink;
        }

        public async Task SendEmailParticipatsAsync(EmailRequest emailRequest)
        {
            try
            {
                List<Participants> participants = JsonSerializer.Deserialize<List<Participants>>(emailRequest.ListOfEmployee);
                ForecastValues formvalues = JsonSerializer.Deserialize<ForecastValues>(emailRequest.FormValues);
                Console.WriteLine("-------------------------------------");
                Console.WriteLine($"Training Theme: {formvalues.TrainingTheme}");
                Console.WriteLine($"Training Place: {formvalues.TrainingPlace}");
                Console.WriteLine($"Training Objective: {formvalues.TrainingObjective}");
                Console.WriteLine($"Start Date: {formvalues.StartDate.ToShortDateString()}");
                Console.WriteLine($"End Date: {formvalues.EndDate.ToShortDateString()}");
                Console.WriteLine($"Start Time: {formvalues.StartTime}");
                Console.WriteLine($"End Time: {formvalues.EndTime}");

                Dictionary<string, string> placeholders = ConvertForecastValuesToPlaceholders(formvalues);
                IFormFile file = emailRequest.File;
                string subject = "Convocation à la formation " + formvalues.TrainingTheme;
                Console.WriteLine(subject);
                Console.WriteLine("coucou");
                if (participants == null || participants.Count == 0)
                {
                    Console.WriteLine("Aucun participant trouvé.");
                    return;  // Sortir de la fonction si la liste est vide
                }

                Console.WriteLine("email envoyer");
                //foreach (Participants employee in participants)
                //{
                //    Console.WriteLine("Sending email to: " + employee.Email);
                //    string emailBody = GetEmailTemplate(employee.FirstName, placeholders);
                //    await SendEmailAsync(employee.Email, subject, emailBody, file);
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SendEmailParticipatsAsync: {ex.Message}");
                if (ex.InnerException != null) { throw new ArgumentException("InnerStackTrace:" + ex.InnerException.Message); }
                else { throw new ArgumentException("InnerStackTrace:" + ex.Message); }
            }
        }

        public async Task SendEmailAsync(string toEmail, string subject, string emailBody, IFormFile file)
        {
            try
            {
                var smtpSettings = _configuration.GetSection("EmailConfiguration");
                Console.WriteLine("Sending email with body: " + emailBody);

                using (var client = new SmtpClient(smtpSettings["SmtpServer"], int.Parse(smtpSettings["Port"])))
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(smtpSettings["Username"], smtpSettings["Password"]);
                    client.EnableSsl = bool.Parse(smtpSettings["EnableSsl"]);

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(smtpSettings["From"], smtpSettings["SenderName"]),
                        Subject = subject,
                        Body = emailBody,
                        IsBodyHtml = false
                    };
                    mailMessage.To.Add(toEmail);
                    if (file != null)
                    {
                        Console.WriteLine("Attachment size: " + file.Length);
                    }
                    else
                    {
                        Console.WriteLine(":no file ");

                    }
                    var fileStream = file.OpenReadStream();

                    // Créer une pièce jointe avec le flux
                    var attachment = new Attachment(fileStream, file.FileName, file.ContentType);
                    mailMessage.Attachments.Add(attachment);
                    Console.WriteLine("Attachment added successfully.");
                    //attachment?.Dispose();
                    Console.WriteLine("Sending email to " + toEmail);
                    await client.SendMailAsync(mailMessage);
                    Console.WriteLine("Email sent to " + toEmail);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SendEmailAsync: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                if (ex.InnerException != null) { throw new ArgumentException("InnerStackTrace:" + ex.InnerException.Message); }
                else { throw new ArgumentException("InnerStackTrace:" + ex.Message); }
            } 
        }

        public string GetEmailTemplate(string email, Dictionary<string, string> placeholders)
        {
            try
            {
                var smtpSettings = _configuration.GetSection("EmailConfiguration");
                string template = File.ReadAllText("Template/TrainingInvitationTemplate.txt");
                template = template.Replace("{EmployeeName}", email);
                template = template.Replace("{ContactPhone}", smtpSettings["contact"]);

                foreach (var placeholder in placeholders)
                {
                    template = template.Replace($"{{{placeholder.Key}}}", placeholder.Value);
                }
                return template;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetEmailTemplate: {ex.Message}");
                // Handle the exception (e.g., return a default message or rethrow)
                return string.Empty;
            }
        }

        private Dictionary<string, string> ConvertForecastValuesToPlaceholders(ForecastValues formValues)
        {
            try
            {
                Console.WriteLine("Converting forecast values to placeholders.");
                return new Dictionary<string, string>
                {
                    { "TrainingTheme", formValues.TrainingTheme },
                    { "TrainingPlace", formValues.TrainingPlace },
                    { "TrainingObjective", formValues.TrainingObjective },
                    { "StartDate", formValues.StartDate.ToString("yyyy-MM-dd") },
                    { "EndDate", formValues.EndDate.ToString("yyyy-MM-dd") },
                    { "StartTime", formValues.StartTime.ToString(@"hh\:mm") },
                    { "EndTime", formValues.EndTime.ToString(@"hh\:mm") }
                };
            }
            catch (Exception ex)
            {
                return new Dictionary<string, string>();
            }
        }

        //____________________________________________________________________________________________________________________________________________________________

        public async Task SendLinkEmailParticipatsAsync(int trainingId,int trainingSessionId,string trainingTheme, List<Participants> participants)
        {
            try
            {
                string subject = "Votre avis sur l'évaluation de la formation :";
                if (participants == null || participants.Count == 0)
                {
                    Console.WriteLine("Aucun participant trouvé.");
                    throw new ArgumentException("Aucun participant trouvé.");
                }
                foreach (Participants employee in participants)
                {
                    var link = await _generateJwtTokenForLink.GenerateJwtTokenForLinkFunction(trainingId, trainingSessionId, trainingTheme,employee.Email);
                    Console.WriteLine("_generateJwtTokenForLink:" + link);
                    //await SendLinklAsync(employee.Email, subject, link);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SendEmailParticipatsAsync: {ex.Message}");
                if (ex.InnerException != null) { throw new ArgumentException("InnerStackTrace:" + ex.InnerException.Message); }
                else { throw new ArgumentException("InnerStackTrace:" + ex.Message); }
            }
        }

        public async Task SendLinklAsync(string toEmail, string subject, string emailBody)
        {
            try
            {
                var smtpSettings = _configuration.GetSection("EmailConfiguration");
                Console.WriteLine("Sending email with body: " + emailBody);
                using (var client = new SmtpClient(smtpSettings["SmtpServer"], int.Parse(smtpSettings["Port"])))
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(smtpSettings["Username"], smtpSettings["Password"]);
                    client.EnableSsl = bool.Parse(smtpSettings["EnableSsl"]);
                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(smtpSettings["From"], smtpSettings["SenderName"]),
                        Subject = subject,
                        Body = emailBody,
                        IsBodyHtml = false
                    };
                    mailMessage.To.Add(toEmail);
                    Console.WriteLine("Sending email to " + toEmail);
                    await client.SendMailAsync(mailMessage);
                    Console.WriteLine("Email sent to " + toEmail);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SendEmailAsync: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                if (ex.InnerException != null) { throw new ArgumentException("InnerStackTrace:" + ex.InnerException.Message); }
                else { throw new ArgumentException("InnerStackTrace:" + ex.Message); }
            }
        }
    }
}
