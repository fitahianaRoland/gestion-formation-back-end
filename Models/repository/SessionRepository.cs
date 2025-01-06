using GestionFormation.Models.classes;
using Microsoft.EntityFrameworkCore;

namespace GestionFormation.Models.repository
{
    public class SessionRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SessionRepository> _logger;

        public SessionRepository(ApplicationDbContext context, ILogger<SessionRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Session>> FindAll()
        {
            return await _context.session.ToListAsync() ?? new List<Session>();
        }

        public async Task<Session?> FindById(int id)
        {
            return await _context.session.FindAsync(id);
        }

        public async Task<List<Session>> GetByTraining(int training_id, int state)
        {
            return await _context.session
                                .Where(ts => ts.TrainingId == training_id && ts.ValidationId == state)
                                .ToListAsync();
        }

        public async Task<Session?> UpdateState(int id, int state, string motif)
        {
            var existingTSession = await FindById(id);
            if (existingTSession == null)
            {
                return null;
            }

            existingTSession.ValidationId = state;
            existingTSession.ReasonRefusal = motif;

            await _context.SaveChangesAsync();
            return existingTSession;
        }

        public async Task<List<Training>> GetValidSessionAsync(int state)
        {
            string sqlQuery = $@"
                SELECT DISTINCT t.*
                FROM training t
                JOIN training_session tr ON t.id = tr.training_id
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

        // GetTrainingBySessionId
        public async Task<List<Session>?> GetTrainingBySessionId(int id)
        {
            return await _context.session.Where(e => e.TrainingId == id).ToListAsync();
        }
        //create
        public async Task<Session> Add(Session trainingSession)
        {
            try
            {
                await _context.AddAsync(trainingSession);
                await _context.SaveChangesAsync();
                return trainingSession;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating training session: {ex.Message}");
                throw;
            }
        }

        // UPDATE
        public async Task<Session?> Update(int id, Session updatedTrainingSession)
        {
            var existingTrainingSession = await FindById(id);
            if (existingTrainingSession == null)
            {
                return null;
            }

            // Mise à jour des propriétés
            existingTrainingSession.TrainingId = updatedTrainingSession.TrainingId;
            existingTrainingSession.StartDate = updatedTrainingSession.StartDate;
            existingTrainingSession.EndDate = updatedTrainingSession.EndDate;

            await _context.SaveChangesAsync();
            return existingTrainingSession;
        }

        // DELETE
        public async Task<bool> DeleteTrainingSessionAsync(int id)
        {
            var trainingSession = await FindById(id);
            if (trainingSession == null)
            {
                return false;
            }

            _context.session.Remove(trainingSession);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
