using gestion_fomation_back_end_local.data;
using gestion_fomation_back_end_local.Models.models;
using Microsoft.EntityFrameworkCore;

namespace gestion_fomation_back_end_local.Models.repository
{
    public class TrainingRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TrainingRepository> _logger;

        public TrainingRepository(ApplicationDbContext context, ILogger<TrainingRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET TRAINING WITH DEPARTMENT
        public async Task<List<TrainingWithDepartment>> GetTrainingsWithDepartments(int? departmentId = null)
        {
            var query = from training in _context.Training
                        join department in _context.Department on training.DepartmentId equals department.DepartmentId
                        where !departmentId.HasValue || training.DepartmentId == departmentId.Value
                        select new TrainingWithDepartment
                        {
                            Id = training.Id,
                            DepartmentId = training.DepartmentId,
                            TrainerTypeId = training.TrainerTypeId,
                            Theme = training.Theme,
                            Objective = training.Objective,
                            Place = training.Place,
                            TrainerName = training.TrainerName,
                            MinNbr = training.MinNbr,
                            MaxNbr = training.MaxNbr,
                            CreationDate = training.CreationDate,
                            DepartmentName = department.DepartmentName
                        };
            return await query.ToListAsync();
        }

        // CREATE
        public async Task<Training> CreateTrainingAsync(Training training)
        {
            try
            {
                await _context.AddAsync(training);
                await _context.SaveChangesAsync();
                return training;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating training: {ex.Message}");
                throw;
            }
        }

        // READ (Get all trainings)
        public async Task<List<Training>> GetAllTrainingsAsync()
        {
            return await _context.Training.ToListAsync();
        }

        // READ (Get training by ID)
        public async Task<Training?> GetTrainingByIdAsync(int id)
        {
            return await _context.Training.FindAsync(id);
        }

        // UPDATE
        public async Task<Training?> UpdateTrainingAsync(int id, Training updatedTraining)
        {
            var existingTraining = await GetTrainingByIdAsync(id);
            if (existingTraining == null)
            {
                return null;
            }

            // Mise à jour des propriétés
            existingTraining.DepartmentId = updatedTraining.DepartmentId;
            existingTraining.TrainerTypeId = updatedTraining.TrainerTypeId;
            existingTraining.Theme = updatedTraining.Theme;
            existingTraining.Objective = updatedTraining.Objective;
            existingTraining.Place = updatedTraining.Place;
            existingTraining.TrainerName = updatedTraining.TrainerName;
            existingTraining.MinNbr = updatedTraining.MinNbr;
            existingTraining.MaxNbr = updatedTraining.MaxNbr;
            existingTraining.CreationDate = updatedTraining.CreationDate;

            await _context.SaveChangesAsync();
            return existingTraining;
        }

        // DELETE
        public async Task<bool> DeleteTrainingAsync(int id)
        {
            var training = await GetTrainingByIdAsync(id);
            if (training == null)
            {
                return false;
            }

            _context.Training.Remove(training);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
