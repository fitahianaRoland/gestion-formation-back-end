using Microsoft.AspNetCore.Mvc;
using GestionFormation.Models.repository;
using Microsoft.AspNetCore.Authorization;
using GestionFormation.Models.classes;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TrainerController : ControllerBase
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;
    private readonly DepartementRepository _departementRepository;
    private readonly TrainerRepository _trainerRepository;
    private readonly TrainerTypeRepository _trainertypeRepository;
    private readonly TrainingRepository _trainingRepository;
    private readonly TrainingOrganizationRepository _trainingOrganizationRepository;

    public TrainerController(
        ApplicationDbContext context,
        ILogger<HomeController> logger,
        DepartementRepository dept,
        TrainerRepository trainer,
        TrainingOrganizationRepository trainingOrganization,
        TrainerTypeRepository trainerType,
        TrainingRepository training,
        SessionRepository sessionRepository
     )
    {
        _context = context;
        _logger = logger;
        _departementRepository = dept;
        _trainerRepository = trainer;
        _trainingOrganizationRepository = trainingOrganization;
        _trainertypeRepository = trainerType;
        _trainingRepository = training;
    }

    [HttpPost("newtrainer")]
    public async Task<IActionResult> CreateTrainer(string? trainer)
    {
        var newtrainer = new Trainer()
        {
            Nom = trainer
        };

        await _trainerRepository.Add(newtrainer);

        return Ok(newtrainer);
    }

    [HttpGet("trainer")]
    public async Task<IActionResult> GetAllTrainer()
    {
        var trai = await _trainerRepository.FindAll();
        return Ok(trai);
    }

    [HttpPost("newtrainingOrganization")]
    public async Task<IActionResult> CreateTrainingOrganization(string? training)
    {
        var newtraining = new TrainingOrganization()
        {
            Nom = training,
        };

        await _trainingOrganizationRepository.Add(newtraining);

        return Ok(newtraining);
    }

    [HttpDelete("deleteTrainer")]
    public async Task<IActionResult> DeleteTrainer(int id)
    {
        var deleted = await _trainerRepository.Delete(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("deleteTraining")] //organization
    public async Task<IActionResult> DeleteTraining(int id)
    {
        var deleted = await _trainingOrganizationRepository.Delete(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpGet("trainingOrganization")]
    public async Task<IActionResult> GetAllTrainingOrganization()
    {
        var trai = await _trainingOrganizationRepository.FindAll();
        return Ok(trai);
    }

    [HttpPut("updatetrainer/{id}")]
    public async Task<IActionResult> UpdateTrainer(int id, string? newName)
    {
        var existingTrainer = await _trainerRepository.FindById(id);

        if (!string.IsNullOrEmpty(newName))
        {
            existingTrainer.Nom = newName;
        }
        else
        {
            return BadRequest("Le nom du formateur ne peut pas être vide.");
        }

        await _trainerRepository.Update(existingTrainer);
        return NoContent();
    }


    [HttpPut("updateTrainingOrganization/{id}")]
    public async Task<IActionResult> UpdateTrainingOrganization(int id, string? newName)
    {
        var existingTraining = await _trainingOrganizationRepository.FindById(id);

        if (!string.IsNullOrEmpty(newName))
        {
            existingTraining.Nom = newName;
        }
        else
        {
            return BadRequest("Le nom du training organization ne peut pas être vide.");
        }

        await _trainingOrganizationRepository.Update(existingTraining);
        return NoContent();
    }
}

