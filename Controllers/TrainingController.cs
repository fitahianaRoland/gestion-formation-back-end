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
using GestionFormation.Repository;

[Route("api/[controller]")]
[ApiController]
public class TrainingController : ControllerBase
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;
    private readonly TrainingRepository _trainingRepository;
    private readonly SessionRepository _sessionrepository;
    private readonly IEmailService _emailService;
    private readonly EmailTemplateService _emailTemplateService;
    private readonly ForecastPresenceRepository _forecastPresenceRepository;
    private readonly TrainingEvaluationTypeRepository _trainingEvaluationTypeRepository;
    private readonly ViewTrainingEvaluationStatusRepository _viewTrainingEvaluationStatusRepository;
    private readonly ViewTrainingSessionEvaluationStatusRepository _viewTrainingSessionEvaluationStatusRepository;
    private readonly SendingStatusRepository _sendingStatusRepository;
    private readonly TrainingSessionStatusRepository _trainingSessionStatusRepository;
    private readonly int _already_planned;
    private readonly int _already_completed;

    public TrainingController(
        ApplicationDbContext context,
        ILogger<HomeController> logger,
        TrainingRepository training,
        SessionRepository sessionRepository,
        IEmailService emailService,
        EmailTemplateService emailTemplateService,
        ForecastPresenceRepository forecastPresenceRepository,
        TrainingEvaluationTypeRepository trainingEvaluationTypeRepository,
        ViewTrainingEvaluationStatusRepository viewTrainingEvaluationStatusRepository,
        ViewTrainingSessionEvaluationStatusRepository viewTrainingSessionEvaluationStatusRepository,
        SendingStatusRepository sendingStatusRepository,
        TrainingSessionStatusRepository trainingSessionStatusRepository
     )
    {
        _context = context;
        _logger = logger;
        _trainingRepository = training;
        _sessionrepository = sessionRepository;
        _emailService = emailService;
        _emailTemplateService = emailTemplateService;
        _forecastPresenceRepository = forecastPresenceRepository;
        _trainingEvaluationTypeRepository = trainingEvaluationTypeRepository;
        _viewTrainingEvaluationStatusRepository = viewTrainingEvaluationStatusRepository;
        _viewTrainingSessionEvaluationStatusRepository = viewTrainingSessionEvaluationStatusRepository;
        _sendingStatusRepository = sendingStatusRepository;
        _trainingSessionStatusRepository = trainingSessionStatusRepository;
        _already_planned = 10;
        _already_completed = 20;
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
        IFormFile? file = emailRequest.File;
        Console.WriteLine($"ListOfEmployee: {emailRequest.ListOfEmployee}");
        Console.WriteLine($"FormValues: {emailRequest.FormValues}");
        Console.WriteLine($"SendEmail: {emailRequest.SendEmail}");
        Console.WriteLine($"File: {emailRequest.File?.FileName}");
        try
        {
            if(file == null)
            {
                throw new ArgumentException("ajouter une pièce jointe");
            }
            // Envoi du message
            if (isSendEmail == true)
            {
                await _forecastPresenceRepository.AddForecast(emailRequest);
                //await _emailService.SendEmailParticipatsAsync(emailRequest);
                int idStatus = await _trainingSessionStatusRepository.GetIdByValueAsync(_already_planned);
                await _trainingRepository.InsertTrainingSessionPlannedStatus(emailRequest.TrainingId,emailRequest.TrainingSessionId,idStatus);
                return Ok("Participants enregistré et Email envoyé avec succès !");
            }
            else
            {
                await _forecastPresenceRepository.AddForecast(emailRequest);
                return Ok(" Participants enregistré ! ");
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(" *** "+ ex.Message +" *** ");
            return BadRequest(new { message = ex.Message} );
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception caught in CreateTestMessage(): {0}", ex.ToString());
            return BadRequest("Erreur: " + ex.Message);
        }
    }

    [HttpGet("ListEmployeeForecast")]
    public async Task<IActionResult> GetListEmployeeForecast(int trainingId,int trainingSessionId, int state,string name = null)
    {
        try
        {
            Console.WriteLine(" id " + trainingId + " idsession " + trainingSessionId + " nom: "+ name);
            var listEmployeeForecast = await _forecastPresenceRepository.ListForecast(trainingId,trainingSessionId,name,state);
            foreach (var i in listEmployeeForecast)
            {
                Console.WriteLine("-------------"+i);
            }
            return Ok(listEmployeeForecast);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("PresenceListEmployee")]
    public async Task<IActionResult> AddPresenceListEmployee([FromBody] PresenceRequest presenceRequest)
    {
        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                int trainingId = presenceRequest.TrainingId;
                int trainingSessionId = presenceRequest.TrainingSessionId;
                int[] listId = presenceRequest.ListId;
                if (!listId.Any()) { throw new ArgumentException("ajouter des participants !"); }
                foreach (int id in listId)
                {
                    Console.WriteLine(" id " + id);
                }
                Console.WriteLine(" id " + trainingId + " idsession " + trainingSessionId + " list des id " + listId);
                await _forecastPresenceRepository.Presence(trainingId, trainingSessionId, listId);
                int idStatus = await _trainingSessionStatusRepository.GetIdByValueAsync(_already_completed);
                await _trainingRepository.InsertTrainingSessionCompletedStatus(presenceRequest.TrainingId, presenceRequest.TrainingSessionId, idStatus);
                await transaction.CommitAsync();
                return Ok(" enregistré ! ");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await transaction.RollbackAsync();
                return BadRequest(new { message = ex.Message});
            }
        }
    }

    [HttpPost("AddNewEvaluationType")]
    public async Task<IActionResult> AddNewEvaluationType([FromBody] string description)
    {
        try
        {
            if(description == "")
            {
                return BadRequest("inserer un description !");
            }
            Console.WriteLine(" hello "+ description);
            TrainingEvaluationType trainingEvaluationType = new TrainingEvaluationType();
            trainingEvaluationType.Name = description;
            await _trainingEvaluationTypeRepository.Add(trainingEvaluationType);
            return Ok("insertion réussi ! ");
        }
        catch (Exception ex)
        {
            return BadRequest("erreur lors de l'insertion"+ex.Message);
        }
    }
    [HttpGet("GetEvaluationType")]
    public async Task<IActionResult> GetEvaluationType()
    {
        List<TrainingEvaluationType> listTrainingEvaluationType = await _trainingEvaluationTypeRepository.FindAll();
        if(listTrainingEvaluationType == null)
        {
            return BadRequest("il n'y a pas de liste !");
        }
        return Ok(listTrainingEvaluationType);
    }

    [HttpDelete("DeleteEvaluationType")]
    public async Task<IActionResult> DeleteEvaluationType(int id)
    {
        var rep = await _trainingEvaluationTypeRepository.Delete(id);
        Console.WriteLine(" reponse evaluation " + rep);
        return Ok("supression avec succes ! ");
    }
    //evaluation
    [HttpGet("GetAllTrainingsWithStatus/{state}/{isFilter}")]
    public async Task<IActionResult> GetAllTrainingsWithStatus(int state, bool isFilter = false)
    {
        var trainings = await _viewTrainingEvaluationStatusRepository.FindByState(isFilter,state);
        var sendingStatus = await _sendingStatusRepository.GetAllAsync();
        return Ok(new
        {
            Trainings = trainings,
            SendingStatus = sendingStatus
        });
    }
    //evaluation
    [HttpGet("GetAllTrainingSessionWithStatus/{trainingId}")]
    public async Task<IActionResult> GetAllTrainingSessionWithStatus(int trainingId)
    {
        var trainings = await _viewTrainingSessionEvaluationStatusRepository.FindByIdTraining(trainingId);
        return Ok(trainings);
    }

    //forecast
    [HttpGet("GetTrainingPlanned")]
    public async Task<IActionResult> GetTrainingPlanned()
    {
        var trainings = await _trainingRepository.GetViewTrainingPlannedStatus();
        return Ok(trainings);
    }
    //forecast
    [HttpGet("GetTrainingSessionPlanned/{trainingId}")]
    public async Task<IActionResult> GetTrainingSessionPlanned(int trainingId)
    {
        var trainings = await _trainingRepository.GetViewTrainingSessionPlannedStatus(trainingId);
        return Ok(trainings);
    }
    //realization
    [HttpGet("GetTrainingCompleted")]
    public async Task<IActionResult> GetTrainingCompleted()
    {
        var trainings = await _trainingRepository.GetViewTrainingCompletedStatus();
        return Ok(trainings);
    }

    //realization
    [HttpGet("GetTrainingSessionCompleted/{trainingId}")]
    public async Task<IActionResult> GetTrainingSessionCompleted(int trainingId)
    {
        var trainings = await _trainingRepository.GetViewTrainingSessionCompletedStatus(trainingId);
        return Ok(trainings);
    }


}