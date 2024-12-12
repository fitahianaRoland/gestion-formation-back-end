using GestionFormation.Models.classes;
using Microsoft.EntityFrameworkCore;

namespace GestionFormation.Repository
{
    public class TrainingEvaluationStatusRepository
    {
        private readonly ApplicationDbContext _context;

        public TrainingEvaluationStatusRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TrainingEvaluationStatus> AddAsync(TrainingEvaluationStatus evaluationStatus)
        {
            if (evaluationStatus == null)
            {
                throw new ArgumentNullException(nameof(evaluationStatus));
            }

            try
            {
                await _context.trainingEvaluationStatus.AddAsync(evaluationStatus);
                await _context.SaveChangesAsync();
                return evaluationStatus;
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is Microsoft.Data.SqlClient.SqlException sqlEx)
                {
                    if (sqlEx.Number == 2627 || sqlEx.Number == 2601)
                    {
                        Console.WriteLine("Violation de contrainte UNIQUE, entrée ignorée.");
                        return evaluationStatus;
                    }
                }
                throw;
            }
        }

    }
}
