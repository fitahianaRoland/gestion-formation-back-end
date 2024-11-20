using Microsoft.AspNetCore.Mvc;
using GestionFormation.Models.repository;
using Microsoft.AspNetCore.Authorization;
using GestionFormation.Models.classes;
using GestionFormation.Interfaces;
using GestionFormation.Services;
using Microsoft.AspNetCore.Routing.Template;
using System.Net.Mail;
using System.Net;
using Newtonsoft.Json;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Administrator")]
public class TrainingController : ControllerBase
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;
    private readonly TrainingRepository _trainingRepository;
    private readonly SessionRepository _sessionrepository;
    private readonly IEmailService _emailService;
    private readonly EmailTemplateService _emailTemplateService;
    private readonly ForecastPresenceRepository _forecastPresenceRepository;

    public TrainingController(
        ApplicationDbContext context,
        ILogger<HomeController> logger,
        TrainingRepository training,
        SessionRepository sessionRepository,
        IEmailService emailService,
        EmailTemplateService emailTemplateService,
        ForecastPresenceRepository forecastPresenceRepository
     )
    {
        _context = context;
        _logger = logger;
        _trainingRepository = training;
        _sessionrepository = sessionRepository;
        _emailService = emailService;
        _emailTemplateService = emailTemplateService;
        _forecastPresenceRepository = forecastPresenceRepository;
    }

    [HttpPut("session/{id}")]
    public async Task<IActionResult> UpdateSession(int id, Session session)
    {
        var updatedsession = await _sessionrepository.Update(id, session);
        if (updatedsession == null)
        {
            return NotFound();
        }
        return Ok(updatedsession);
    }

    [HttpGet("session")]
    public async Task<IActionResult> GetAllSession()
    {
        var session = await _sessionrepository.FindAll();
        return Ok(session);
    }

    [HttpGet("session/{id}")]
    public async Task<IActionResult> GetSessionById(int id)
    {
        var session = await _sessionrepository.FindById(id);
        if (session == null)
        {
            return NotFound();
        }
        return Ok(session);
    }
    [HttpGet("TrainingSession/{trainingId}")]
    public async Task<IActionResult> GetTrainingBySessionId(int trainingId)
    {
        var session = await _sessionrepository.GetTrainingBySessionId(trainingId);
        if (session == null)
        {
            return NotFound();
        }
        return Ok(session);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTraining(int id, Training training)
    {
        var updatedTraining = await _trainingRepository.Update(id, training);
        if (updatedTraining == null)
        {
            return NotFound();
        }
        return Ok(updatedTraining);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTrainingById(int id)
    {
        var training = await _trainingRepository.FindById(id);
        if (training == null)
        {
            return NotFound();
        }
        return Ok(training);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTrainings()
    {
        var trainings = await _trainingRepository.FindAll();
        return Ok(trainings);
    }

    [HttpGet("withDepartments")]
    public async Task<IActionResult> GetTrainingsWithDepartments(int? departmentId = null)
    {
        var trainingsWithDepartments = await _trainingRepository.GetTrainingsWithDepartments(departmentId);
        return Ok(trainingsWithDepartments);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTraining(int id)
    {
        var deleted = await _trainingRepository.Delete(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpPost("addFormation")]
    public async Task<IActionResult> AddFormation(int departement, int trainertype, string theme, string objective, string place, string trainer, int min, int max, DateTime creation, List<Session> sessions)
    {
        var trainingva = new Training()
        {
            //DepartementID = departement,
            TrainerTypeID = trainertype,
            Theme = theme,
            Objective = objective,
            Place = place,
            TrainerName = trainer,
            MinNbr = min,
            MaxNbr = max,
            Creation = creation.Date
        };

        try
        {
            if (min > max)
            {
                throw new Exception("Le nombre maximum ne doit pas être inférieur au minimum.");
            }

            await _trainingRepository.Add(trainingva);

            int generatedId = trainingva.Id;


            foreach (var sessionDto in sessions)
            {
                var sessioni = new Session()
                {
                    TrainingId = generatedId,
                    StartDate = sessionDto.StartDate,
                    EndDate = sessionDto.StartDate
                };

                await _sessionrepository.Add(sessioni);
            }

            return Ok(new
            {
                Training = trainingva,
                Message = $"{sessions.Count} sessions have been added successfully."
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }

    }

    [HttpPost("send-email")]
    public async Task<IActionResult> CreateTestMessage([FromForm] EmailRequest emailRequest)
    {
        bool isSendEmail = emailRequest.SendEmail;
        IFormFile file = emailRequest.File;
        Console.WriteLine($"ListOfEmployee: {emailRequest.ListOfEmployee}");
        Console.WriteLine($"FormValues: {emailRequest.FormValues}");
        Console.WriteLine($"SendEmail: {emailRequest.SendEmail}");
        Console.WriteLine($"File: {emailRequest.File?.FileName}");
        try
        { 
            // Envoi du message
            if (isSendEmail == true)
            {
                //await _forecastPresenceRepository.AddForecast(emailRequest);
                //await _emailService.SendEmailParticipatsAsync(emailRequest);
                return Ok("Participants enregistré et Email envoyé avec succès !");
            }
            else {
                //await _forecastPresenceRepository.AddForecast(emailRequest);
                return Ok(" Participants enregistré ! "); 
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception caught in CreateTestMessage(): {0}", ex.ToString());
            return BadRequest("Erreur: " + ex.Message);
        }
    }

}
