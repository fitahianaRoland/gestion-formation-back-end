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
    public async Task<IActionResult> GetTraining()
    {
        var training = await _trainingRequestRepository.GetValidTrainingRequestsAsync();
        if (training == null)
        {
            return NotFound();
        }
        return Ok(training);
    }


    [HttpGet("nombresession")]
    public async Task<IActionResult> GetNombreSession(int trainingID)
    {
        int sessions = 0; 

        try
        {
            sessions = await _trainingRequestRepository.GetNbSession(trainingID);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur : {ex.Message}");
            return BadRequest(new { message = ex.Message }); 
        }

        return Ok(sessions);
    }


    [HttpGet("enattente")]
    public async Task<IActionResult> GetRequestTraining(int trainingID)
    {
        var training = await _trainingRequestRepository.GetRequestByTraining(trainingID);
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
                RequestDate = request.RequestDate
            };

            await _trainingRequestRepository.Add(trainingRequest);
        }

        return Ok($"Insertion de {requests.Count} demandes de formation réussie.");
    }

}

