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

            // Vérifie si une entrée similaire existe déjà
            var existingStatus = await _context.trainingEvaluationStatus
                .FirstOrDefaultAsync(s => s.TrainingId == evaluationStatus.TrainingId &&
                                          s.TrainingSessionId == evaluationStatus.TrainingSessionId &&
                                          s.SendingStatusId == evaluationStatus.SendingStatusId);

            if (existingStatus != null)
            {
                Console.WriteLine("Une entrée avec les mêmes valeurs existe déjà. Ignorée.");
                return existingStatus; // Retourne l'entrée existante pour éviter l'exception
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
                        Console.WriteLine("***************************************************************************************************************************************" +
                            "*********************************************************************************************************************************");
                        Console.WriteLine($"Violation de contrainte UNIQUE ignorée : {sqlEx.Message}");
                        return evaluationStatus;
                    }
                }
                throw;
            }
        }

    }

}
