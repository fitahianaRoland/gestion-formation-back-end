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

        public async Task<Session> FindById(int id)
        {
            return await _context.session.FindAsync(id);
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
