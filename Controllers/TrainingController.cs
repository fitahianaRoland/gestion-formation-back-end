using Microsoft.AspNetCore.Mvc;
using GestionFormation.Models.repository;
using Microsoft.AspNetCore.Authorization;
using GestionFormation.Models.classes;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TrainingController : ControllerBase
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;
    private readonly TrainingRepository _trainingRepository;
    private readonly SessionRepository _sessionrepository;

    public TrainingController(
        ApplicationDbContext context,
        ILogger<HomeController> logger,
        TrainingRepository training,
        SessionRepository sessionRepository
     )
    {
        _context = context;
        _logger = logger;
        _trainingRepository = training;
        _sessionrepository = sessionRepository;
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
            DepartementID = departement,
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
}
