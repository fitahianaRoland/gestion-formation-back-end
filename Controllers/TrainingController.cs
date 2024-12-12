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

    public TrainingController(
        ApplicationDbContext context,
        ILogger<HomeController> logger,
        TrainingRepository training
     )
    {
        _context = context;
        _logger = logger;
        _trainingRepository = training;
    }

    [HttpGet("state")]
    public async Task<IActionResult> GetTrainingByState(int state)
    {
        var training = await _trainingRepository.FindByState(state);
        if (training == null)
        {
            return NotFound();
        }
        return Ok(training);
    }

    [HttpPut("stateUpdate/{id}")]
    public async Task<IActionResult> UpdateTrainingState(int id, int state)
    {
        var updatedTraining = await _trainingRepository.UpdateState(id, state);
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

    [HttpPost("AddTraining")]
    public async Task<IActionResult> AddTraining([FromBody] Training request)
    {
        var trainingva = new Training()
        {
            DepartementID = request.DepartementID,
            TrainerTypeID = request.TrainerTypeID,
            Theme = request.Theme,
            Objective = request.Objective,
            Place = request.Place,
            MinNbr = request.MinNbr,
            MaxNbr = request.MaxNbr,
            Creation = request.Creation,
            Validation = 1,
        };

        try
        {
            if (request.MinNbr > request.MaxNbr)
            {
                throw new Exception("Le nombre maximum ne doit pas être inférieur au minimum.");
            }

            await _trainingRepository.Add(trainingva);

            return Ok(trainingva);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

}
