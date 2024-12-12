using GestionFormation.Models.classes;
using GestionFormation.Models.repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
[ApiController]
[Authorize]
public class SendEmailController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly EnvoiEmail envoiEmail;

        public SendEmailController(
            ApplicationDbContext context,
            ILogger<HomeController> logger,
            EnvoiEmail envoi
         )
        {
            _context = context;
            _logger = logger;
            envoiEmail = envoi;
        }

    [HttpPost("SendEmail")]
    public async Task<IActionResult> SendEmail([FromBody] EmailRequest emailRequest)
    {
        if (string.IsNullOrEmpty(emailRequest.To) ||
            string.IsNullOrEmpty(emailRequest.Subject) ||
            string.IsNullOrEmpty(emailRequest.Body))
        {
            return BadRequest("Tous les champs (To, Subject, Body) sont requis.");
        }

        bool isEmailSent = await envoiEmail.SendEmailAsync(
            emailRequest.To, 
            emailRequest.Subject, 
            emailRequest.Body, 
            emailRequest.Cc
            );

        if (isEmailSent)
        {
            return Ok("Email envoyé avec succès !");
        }
        else
        {
            return StatusCode(500, "Erreur lors de l'envoi de l'email.");
        }
    }

}

