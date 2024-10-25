using gestion_fomation_back_end_local.Models.models;
using gestion_fomation_back_end_local.Models.repository;
using Microsoft.AspNetCore.Mvc;

namespace gestion_fomation_back_end_local.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingController : ControllerBase
    {
        private readonly TrainingRepository _trainingRepository;

        public TrainingController(TrainingRepository trainingRepository)
        {
            _trainingRepository = trainingRepository;
        }

        // GET: api/Training
        [HttpGet]
        public async Task<IActionResult> GetAllTrainings()
        {
            var trainings = await _trainingRepository.GetAllTrainingsAsync();
            return Ok(trainings);
        }

        // GET: api/Training/with-departments
        [HttpGet("withDepartments")]
        public async Task<IActionResult> GetTrainingsWithDepartments(int? departmentId = null)
        {
            var trainingsWithDepartments = await _trainingRepository.GetTrainingsWithDepartments(departmentId);
            return Ok(trainingsWithDepartments);
        }


        // GET: api/Training/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrainingById(int id)
        {
            var training = await _trainingRepository.GetTrainingByIdAsync(id);
            if (training == null)
            {
                return NotFound();
            }
            return Ok(training);
        }

        // POST: api/Training
        [HttpPost]
        public async Task<IActionResult> CreateTraining(Training training)
        {
            var createdTraining = await _trainingRepository.CreateTrainingAsync(training);
            return CreatedAtAction(nameof(GetTrainingById), new { id = createdTraining.Id }, createdTraining);
        }

        // PUT: api/Training/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTraining(int id, Training training)
        {
            var updatedTraining = await _trainingRepository.UpdateTrainingAsync(id, training);
            if (updatedTraining == null)
            {
                return NotFound();
            }
            return Ok(updatedTraining);
        }

        // DELETE: api/Training/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTraining(int id)
        {
            var deleted = await _trainingRepository.DeleteTrainingAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
