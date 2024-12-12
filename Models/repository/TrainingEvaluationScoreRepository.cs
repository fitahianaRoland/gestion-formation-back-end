using GestionFormation.Models.classes;
using Microsoft.EntityFrameworkCore;

namespace GestionFormation.Repository
{
    public class TrainingEvaluationScoreRepository
    {
        private readonly ApplicationDbContext _context;

        public TrainingEvaluationScoreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TrainingEvaluationScore> AddAsync(TrainingEvaluationScore score)
        {
            _context.trainingEvaluationScore.Add(score);
            await _context.SaveChangesAsync();
            return score;
        }

        public async Task<List<TrainingEvaluationScore>> GetAllAsync()
        {
            return await _context.trainingEvaluationScore.ToListAsync();
        }
        public async Task<TrainingEvaluationScore> GetByIdAsync(int id)
        {
            return await _context.trainingEvaluationScore.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task AddTrainingEvaluationScoreAsync(int trainingEvaluationId, List<TrainingEvaluationType> trainingEvaluationType)
        {
            if (trainingEvaluationId == null)
            {
                throw new ArgumentException("trainingEvaluationId est null", nameof(trainingEvaluationId));
            }
            if (trainingEvaluationType == null )
            {
                throw new ArgumentException("la liste des scores ne peut pas être vide.", nameof(trainingEvaluationId));
            }

            foreach(var row in trainingEvaluationType)
            {

            }

            var newItems = trainingEvaluationType.Select(typeScore => new TrainingEvaluationScore
            {
                TrainingEvaluationId = trainingEvaluationId,
                TrainingEvaluationTypeId = typeScore.Id,
                Score = typeScore.Rating
            }).ToList();

            _context.trainingEvaluationScore.AddRange(newItems);
            await _context.SaveChangesAsync();
        }
    }
}
