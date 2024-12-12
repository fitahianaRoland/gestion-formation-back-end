using GestionFormation.Models.classes;
using Microsoft.EntityFrameworkCore;

namespace GestionFormation.Models.repository
{
    public class ViewTrainingSessionEvaluationStatusRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TrainingRepository> _logger;

        public ViewTrainingSessionEvaluationStatusRepository(ApplicationDbContext context, ILogger<TrainingRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<ViewTrainingSessionEvaluationStatus>> FindAll()
        {
            return await _context.viewTrainingSessionEvaluationStatus.ToListAsync() ?? new List<ViewTrainingSessionEvaluationStatus>();
        }

        public async Task<List<ViewTrainingSessionEvaluationStatus>> FindByIdTraining(int trainingId)
        {
            return await _context.viewTrainingSessionEvaluationStatus.Where(e => e.TrainingId == trainingId).ToListAsync() ?? new List<ViewTrainingSessionEvaluationStatus>();
        }

    }
}
