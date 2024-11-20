using GestionFormation.Models.classes;
using Microsoft.EntityFrameworkCore;

namespace GestionFormation.Models.repository
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

        public async Task<List<Training>> FindAll()
        {
            return await _context.trainings.ToListAsync() ?? new List<Training>();
        }

        public async Task<Training> FindById(int id)
        {
            return await _context.trainings.FindAsync(id);
        }

        //create
        public async Task<Training> Add(Training training)
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

        public async Task<List<TrainingWithDepartment>> GetTrainingsWithDepartments(int? departmentId = null)
        {
            var query = from training in _context.trainings
                        //join department in _context.Departements on training.DepartementID equals department.Id
                        where !departmentId.HasValue /*|| training.DepartementID == departmentId.Value*/
                        select new TrainingWithDepartment
                        {
                            Id = training.Id,
                            //DepartmentId = training.DepartementID,
                            TrainerTypeId = training.TrainerTypeID,
                            Theme = training.Theme,
                            Objective = training.Objective,
                            Place = training.Place,
                            TrainerName = training.TrainerName,
                            MinNbr = training.MinNbr,
                            MaxNbr = training.MaxNbr,
                            CreationDate = training.Creation,
                            //DepartmentName = department.Nom
                        };
            return await query.ToListAsync();
        }

        //niova ny nomanle fonction any
        public async Task<Training?> Update(int id, Training updatedTraining)
        {
            var existingTraining = await FindById(id);
            if (existingTraining == null)
            {
                return null;
            }

            // Mise à jour des propriétés
            //existingTraining.DepartementID = updatedTraining.DepartementID;
            existingTraining.TrainerTypeID = updatedTraining.TrainerTypeID;
            existingTraining.Theme = updatedTraining.Theme;
            existingTraining.Objective = updatedTraining.Objective;
            existingTraining.Place = updatedTraining.Place;
            existingTraining.TrainerName = updatedTraining.TrainerName;
            existingTraining.MinNbr = updatedTraining.MinNbr;
            existingTraining.MaxNbr = updatedTraining.MaxNbr;
            existingTraining.Creation = updatedTraining.Creation;

            await _context.SaveChangesAsync();
            return existingTraining;
        }

        // niova ihany koa ny nom
        public async Task<bool> Delete(int id)
        {
            var training = await FindById(id);
            if (training == null)
            {
                return false;
            }

            _context.trainings.Remove(training);
            await _context.SaveChangesAsync();
            return true;
        }




    }
}
