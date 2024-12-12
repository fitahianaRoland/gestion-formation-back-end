using GestionFormation.Models.classes;
using GestionFormation.Models.repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
[ApiController]
[Authorize]
public class InternalTrainingController : ControllerBase
{
	private readonly ILogger<HomeController> _logger;
	private readonly ApplicationDbContext _context;
	private readonly InternalTrainingRepository internalTrainingRepository;

	public InternalTrainingController(
		ApplicationDbContext context,
		ILogger<HomeController> logger,
		InternalTrainingRepository intern
	 )
	{
		_context = context;
		_logger = logger;
		internalTrainingRepository = intern;	
	}

	[HttpGet("getall")]
	public async Task<IActionResult> GetAll()
	{
		var interne = await internalTrainingRepository.FindAll();
		if (interne == null)
		{
			return NotFound();
		}
		return Ok(interne);
	}

	[HttpPost("addinternaltraining")]
	public async Task<IActionResult> CreateInternTraining([FromBody] InternalTraining request)
	{
		var intern = new InternalTraining()
		{
			Id = request.Id,
			TrainerName = request.TrainerName,
			TrainingId = request.TrainingId,
			Validation = request.Validation,
		};

		await internalTrainingRepository.Add(intern);

		return Ok(intern);
	}

	[HttpPut("updateState")]
	public async Task<IActionResult> UpdateState(int id,int state)
	{
		var updatedState = await internalTrainingRepository.UpdateState(id, state);
		if (updatedState == null)
		{
			return NotFound();
		}
		return Ok(updatedState);
	}
}

