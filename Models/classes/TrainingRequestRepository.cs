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

        public async Task<List<Training>> GetValidTrainingRequestsAsync()
        {
            string sqlQuery = $@"
                SELECT t.*
                FROM training t
                WHERE EXISTS (
                    SELECT 1
                    FROM training_request tr
                    WHERE tr.training_id = t.id
                );";

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


        public async Task<int> GetRequestByTraining(int trainingid)
        {
            return await _context.training_Requests
                                 .Where(t => t.TrainingID == trainingid)
                                 .CountAsync();
        }


        public async Task<int> GetNbSession(int trainingid)
        {
            int count = await GetRequestByTraining(trainingid);

            var training = await _context.trainings
                                         .Where(t => t.Id == trainingid)
                                         .FirstOrDefaultAsync();

            Console.WriteLine("count" + count);
            Console.WriteLine("training" + training.MaxNbr);

            if (count < training?.MinNbr) {
                throw new Exception("La formation n'a pas le droit de commencer, car le nombre de demandes est insuffisant.");
            }
                
            var jour = (double)count / training.MaxNbr;
            
            var joursArrondi = (int)Math.Ceiling(jour);
            Console.WriteLine("jour" + jour);

            return joursArrondi;
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

