using gestion_fomation_back_end_local.data;
using gestion_fomation_back_end_local.Models.models;
using Microsoft.EntityFrameworkCore;

namespace gestion_fomation_back_end_local.Models.repository
{
    public class TrainingSessionRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TrainingSessionRepository> _logger;

        public TrainingSessionRepository(ApplicationDbContext context, ILogger<TrainingSessionRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        // CREATE
        public async Task<TrainingSession> CreateTrainingSessionAsync(TrainingSession trainingSession)
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

        // READ (Get all training sessions)
        public async Task<List<TrainingSession>> GetAllTrainingSessionsAsync()
        {
            return await _context.TrainingSession.ToListAsync();
        }

        public async Task<List<TrainingSession>> GetAllTrainingSessionsByTrainingIdAsync(int trainingId)
        {
            return await _context.TrainingSession.Where(e => e.TrainingId == trainingId).ToListAsync();
        }
        // READ (Get training session by ID)
        public async Task<TrainingSession?> GetTrainingSessionByIdAsync(int id)
        {
            return await _context.TrainingSession.FindAsync(id);
        }

        // UPDATE
        public async Task<TrainingSession?> UpdateTrainingSessionAsync(int id, TrainingSession updatedTrainingSession)
        {
            var existingTrainingSession = await GetTrainingSessionByIdAsync(id);
            if (existingTrainingSession == null)
            {
                return null;
            }

            // Mise à jour des propriétés
            existingTrainingSession.TrainingId = updatedTrainingSession.TrainingId;
            existingTrainingSession.StartDate = updatedTrainingSession.StartDate;
            existingTrainingSession.EndDate = updatedTrainingSession.EndDate;
            existingTrainingSession.StartHour = updatedTrainingSession.StartHour;
            existingTrainingSession.EndHour = updatedTrainingSession.EndHour;

            await _context.SaveChangesAsync();
            return existingTrainingSession;
        }

        // DELETE
        public async Task<bool> DeleteTrainingSessionAsync(int id)
        {
            var trainingSession = await GetTrainingSessionByIdAsync(id);
            if (trainingSession == null)
            {
                return false;
            }

            _context.TrainingSession.Remove(trainingSession);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
