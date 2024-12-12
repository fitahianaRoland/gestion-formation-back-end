using Microsoft.AspNetCore.Mvc;
using GestionFormation.Models.repository;
using Microsoft.AspNetCore.Authorization;
using GestionFormation.Models.classes;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SessionController : ControllerBase
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;
    private readonly TrainingRepository _trainingRepository;
    private readonly SessionRepository _sessionrepository;

    public SessionController(
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

    [HttpGet("gettraining")]
    public async Task<IActionResult> GetTraining(int state)
    {
        var training = await _sessionrepository.GetValidSessionAsync(state);
        if (training == null)
        {
            return NotFound();
        }
        return Ok(training);
    }

    [HttpPut("stateUpdate/{id}")]
    public async Task<IActionResult> UpdateSessionState(int id, int state)
    {
        var updatedState = await _sessionrepository.UpdateState(id, state);
        if (updatedState == null)
        {

            return NotFound();
        }
        return Ok(updatedState);
    }

    [HttpPost("ajoutsession")]
    public async Task<IActionResult> AddTSession([FromBody] List<Session> sessions)
    {
        foreach (var session in sessions)
        {
            var sessionn = new Session()
            {
                StartDate = session.StartDate,
                EndDate = session.EndDate,
                TrainingId = session.TrainingId,
                Validation = session.Validation
            };

            await _sessionrepository.Add(sessionn);
        }

        return Ok($"L'Insertion de{sessions.Count} sessions à réussie.");
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

}
