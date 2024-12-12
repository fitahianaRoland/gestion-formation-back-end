using GestionFormation.Models.classes;
using Microsoft.EntityFrameworkCore;

namespace GestionFormation.Models.repository
{
    public class TrainingRequestRepository
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<SessionRepository> _logger;

        public TrainingRequestRepository(ApplicationDbContext context, ILogger<SessionRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Training>> GetValidTrainingRequestsAsync(int state)
        {
            string sqlQuery = $@"
                SELECT DISTINCT t.*
                FROM training t
                JOIN training_request tr ON t.id = tr.training_id
                WHERE tr.validation_id = {state}";

            try
            {
                var results = await _context.trainings
                    .FromSqlRaw(sqlQuery)
                    .ToListAsync();

                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de l'exécution de la requête brute.");
                throw;
            }
        }


        public async Task<int> CountByTrainingRequestState(int state, int trainingid)
        {
            return await _context.training_Requests
                                 .Where(t => t.validationID == state && t.TrainingID == trainingid)
                                 .CountAsync();
        }


        public async Task<int> GetNbSession(int state, int trainingid)
        {
            int count = await CountByTrainingRequestState(state, trainingid);

            var training = await _context.trainings
                                         .Where(t => t.Id == trainingid)
                                         .FirstOrDefaultAsync();

            Console.WriteLine("count" + count);
            Console.WriteLine("training" + training.MaxNbr);

            if (count < training.MinNbr) {
                throw new Exception("La formation n'a pas le droit de commencer, car le nombre de demandes est insuffisant.");
            }
                
            var jour = (double)count / training.MaxNbr;
            
            var joursArrondi = (int)Math.Ceiling(jour);
            Console.WriteLine("jour" + jour);

            return joursArrondi;
        }

            public async Task<Training_request> UpdateState(int id, int state)
        {
            var existingTraining = await FindById(id);
            if (existingTraining == null)
            {
                return null;
            }

            existingTraining.validationID = state;

            await _context.SaveChangesAsync();
            return existingTraining;
        }

        public async Task<List<Training_request>> FindByTrainingRequesteState(int state,int training)
        {
            return await _context.training_Requests
                .Where(t => t.validationID == state && t.TrainingID == training).ToListAsync();
        } 

        public async Task<List<Training_request>> FindByState(int state)
        {
            return await _context.training_Requests.Where(t => t.validationID == state).ToListAsync();
        }

        public async Task<List<Training_request>> FindAll()
        {
            return await _context.training_Requests.ToListAsync() ?? new List<Training_request>();
        }

        public async Task<Training_request?> FindById(int id)
        {
            return await _context.training_Requests.FindAsync(id);
        }

        //create
        public async Task<Training_request> Add(Training_request trainingrequest)
        {
            try
            {
                await _context.AddAsync(trainingrequest);
                await _context.SaveChangesAsync();
                return trainingrequest;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating trainingrequest: {ex.Message}");
                throw;
            }
        }

        public async Task<Training_request> Update(int id, Training_request updatedTraining)
        {
            var existingTraining = await FindById(id);
            if (existingTraining == null)
            {
                return null;
            }

            existingTraining.Id = updatedTraining.Id;
            existingTraining.TrainingID = updatedTraining.TrainingID;
            existingTraining.Name = updatedTraining.Name;
            existingTraining.FirstName = updatedTraining.FirstName;
            existingTraining.Email = updatedTraining.Email;
            existingTraining.RequestDate = updatedTraining.RequestDate;
            existingTraining.RefusalDate = updatedTraining.RefusalDate;
            existingTraining.ValidationDate = updatedTraining.ValidationDate;
            existingTraining.Pattern = updatedTraining.Pattern;
            existingTraining.validationID = updatedTraining.validationID;

            await _context.SaveChangesAsync();
            return existingTraining;
        }


        public async Task<bool> Delete(int id)
        {
            var training = await FindById(id);
            if (training == null)
            {
                return false;
            }

            _context.training_Requests.Remove(training);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

