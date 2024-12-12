using GestionFormation.Models.classes;
using GestionFormation.Models.repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


[Route("api/[controller]")]
[ApiController]
[Authorize]
    public class TrainingRequestController : ControllerBase
    {

        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly TrainingRequestRepository _trainingRequestRepository;


    public TrainingRequestController(
            ApplicationDbContext context,
            ILogger<HomeController> logger,
            TrainingRequestRepository trainingRequestRepository 
         )
        {
            _context = context;
            _logger = logger;
            _trainingRequestRepository = trainingRequestRepository;
    }

    [HttpGet("gettraining")]
    public async Task<IActionResult> GetTraining(int state)
    {
        var training = await _trainingRequestRepository.GetValidTrainingRequestsAsync(state);
        if (training == null)
        {
            return NotFound();
        }
        return Ok(training);
    }


    [HttpGet("nombresession")]
    public async Task<IActionResult> GetNombreSession(int state, int trainingID)
    {
        int sessions = 0; 

        try
        {
            sessions = await _trainingRequestRepository.GetNbSession(state, trainingID);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur : {ex.Message}");
            return BadRequest(new { message = ex.Message }); 
        }

        return Ok(sessions);
    }


    [HttpGet("enattente")]
    public async Task<IActionResult> GetTrainingByStateAndTraining(int state, int trainingID)
    {
        var training = await _trainingRequestRepository.FindByTrainingRequesteState(state,trainingID);
        if (training == null)
        {
            return NotFound();
        }
        return Ok(training);
    }

    [HttpPut("stateUpdate")]
    public async Task<IActionResult> UpdateTrainingRequestState(int id, int state)
    {
        var updatedTraining = await _trainingRequestRepository.UpdateState(id, state);
        if (updatedTraining == null)
        {

            return NotFound();
        }
        return Ok(updatedTraining);
    }

    [HttpGet("state")]
    public async Task<IActionResult> GetTrainingByState(int state)
    {
        var training = await _trainingRequestRepository.FindByState(state);
        if (training == null)
        {
            return NotFound();
        }
        return Ok(training);
    }

    [HttpPost("AddTrainingRequest")]
    public async Task<IActionResult> AddTrainingRequests([FromBody] List<Training_request> requests)
    {
        foreach (var request in requests)
        {
            var trainingRequest = new Training_request()
            {
                TrainingID = request.TrainingID,
                Name = request.Name,
                FirstName = request.FirstName,
                Email = request.Email,
                RequestDate = request.RequestDate,
                ValidationDate = null,
                RefusalDate = null,
                Pattern = null,
                validationID = 1
            };

            await _trainingRequestRepository.Add(trainingRequest);
        }

        return Ok($"Insertion de {requests.Count} demandes de formation réussie.");
    }

}

