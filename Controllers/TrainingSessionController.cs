using gestion_fomation_back_end_local.Models.models;
using gestion_fomation_back_end_local.Models.repository;
using Microsoft.AspNetCore.Mvc;

namespace gestion_fomation_back_end_local.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingSessionController : ControllerBase
    {
        private readonly TrainingSessionRepository _trainingSessionRepository;

        public TrainingSessionController(TrainingSessionRepository trainingSessionRepository)
        {
            _trainingSessionRepository = trainingSessionRepository;
        }

        // GET: api/TrainingSession
        [HttpGet]
        public async Task<IActionResult> GetAllTrainingSessions()
        {
            var trainingSessions = await _trainingSessionRepository.GetAllTrainingSessionsAsync();
            return Ok(trainingSessions);
        }

        // GET: api/TrainingSession/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrainingSessionById(int id)
        {
            var trainingSession = await _trainingSessionRepository.GetTrainingSessionByIdAsync(id);
            if (trainingSession == null)
            {
                return NotFound();
            }
            return Ok(trainingSession);
        }

        // POST: api/TrainingSession
        [HttpPost]
        public async Task<IActionResult> CreateTrainingSession(TrainingSession trainingSession)
        {
            var createdTrainingSession = await _trainingSessionRepository.CreateTrainingSessionAsync(trainingSession);
            return CreatedAtAction(nameof(GetTrainingSessionById), new { id = createdTrainingSession.Id }, createdTrainingSession);
        }

        // PUT: api/TrainingSession/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTrainingSession(int id, TrainingSession trainingSession)
        {
            var updatedTrainingSession = await _trainingSessionRepository.UpdateTrainingSessionAsync(id, trainingSession);
            if (updatedTrainingSession == null)
            {
                return NotFound();
            }
            return Ok(updatedTrainingSession);
        }

        // DELETE: api/TrainingSession/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainingSession(int id)
        {
            var deleted = await _trainingSessionRepository.DeleteTrainingSessionAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
