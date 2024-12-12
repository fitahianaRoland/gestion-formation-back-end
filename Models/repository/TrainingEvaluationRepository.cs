using GestionFormation.Models.classes;
using Microsoft.EntityFrameworkCore;

namespace GestionFormation.Repository
{
    public class TrainingEvaluationRepository
    {
        private readonly ApplicationDbContext _context;

        public TrainingEvaluationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TrainingEvaluation> AddAsync(TrainingEvaluation evaluation)
        {
            _context.trainingEvaluation.Add(evaluation);
            //await _context.SaveChangesAsync();
            return evaluation;
        }

        public async Task<List<TrainingEvaluation>> GetAllAsync()
        {
            return await _context.trainingEvaluation.ToListAsync();
        }

        public async Task<TrainingEvaluation> GetByIdAsync(int id)
        {
            return await _context.trainingEvaluation.FirstOrDefaultAsync(e => e.Id == id) ?? new TrainingEvaluation();
        }

        public async Task<List<ViewTrainingEvaluationAverageScore>> GetAverageScore(int training_id,int training_session_id)
        {
            string sql = "select * from view_training_evaluation_average_score where training_id = {0} and training_session_id = {1} ";
            return await _context.viewTrainingEvaluationAverageScores.FromSqlRaw(sql, training_id, training_session_id).ToListAsync();
        }
        public async Task<ViewTrainingEvaluationGeneralAverageScore> GetGeneralAverageScore(int training_id, int training_session_id)
        {
            string sql = "select * from view_training_evaluation_general_average_score where training_id = {0} and training_session_id = {1} ";
            return await _context.viewTrainingEvaluationGeneralAverageScores.FromSqlRaw(sql, training_id, training_session_id).FirstOrDefaultAsync() ?? new ViewTrainingEvaluationGeneralAverageScore();
        }

        public async Task<ViewTrainingEvaluationResponseCount> GetTrainingEvaluationResponseCount(int training_id, int training_session_id)
        {
            string sql = "select * from view_training_evaluation_response_count where training_id = {0} and training_session_id = {1} ";
            return await _context.viewTrainingEvaluationResponseCounts.FromSqlRaw(sql, training_id, training_session_id).FirstOrDefaultAsync() ?? new ViewTrainingEvaluationResponseCount();
        }

        public async Task<List<TrainingEvaluation>> GetComment(int training_id, int training_session_id)
        {
            var comments = await _context.trainingEvaluation.Where(e => e.TrainingId == training_id && e.TrainingSessionId == training_session_id && !string.IsNullOrEmpty(e.Comment)).ToListAsync();
            return comments;
        }

    }
}
