using GestionFormation.Models.classes;
using Microsoft.EntityFrameworkCore;

namespace GestionFormation.Models.repository
{
    public class ViewTrainingEvaluationStatusRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TrainingRepository> _logger;

        public ViewTrainingEvaluationStatusRepository(ApplicationDbContext context, ILogger<TrainingRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<ViewTrainingEvaluationStatus>> FindAll()
        {
            return await _context.viewTrainingEvaluationStatus.ToListAsync() ?? new List<ViewTrainingEvaluationStatus>();
        }

        public async Task<List<ViewTrainingEvaluationStatus>> FindByState(bool isFilter,int state)
        {
            if(!isFilter) { return await _context.viewTrainingEvaluationStatus.ToListAsync() ?? new List<ViewTrainingEvaluationStatus>(); }
            else { return await _context.viewTrainingEvaluationStatus.Where(e => e.SendingStatusValue == state).ToListAsync() ?? new List<ViewTrainingEvaluationStatus>(); }
           
        }

    }
}
