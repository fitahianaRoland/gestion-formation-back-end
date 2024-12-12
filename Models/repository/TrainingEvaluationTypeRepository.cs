using GestionFormation.Models.classes;
using Microsoft.EntityFrameworkCore;

namespace GestionFormation.Models.repository
{
    public class TrainingEvaluationTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public TrainingEvaluationTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TrainingEvaluationType>> FindAll()
        {
            return await _context.TrainingEvaluationTypes.ToListAsync() ?? new List<TrainingEvaluationType>();
        }

        public async Task<TrainingEvaluationType> FindById(int id)
        {
            return await _context.TrainingEvaluationTypes.FindAsync(id);
        }

        public async Task Add(TrainingEvaluationType trainingEvaluationType)
        {
            if (trainingEvaluationType == null)
            {
                throw new ArgumentNullException(nameof(trainingEvaluationType));
            }

            await _context.TrainingEvaluationTypes.AddAsync(trainingEvaluationType);
            await _context.SaveChangesAsync();
        }

        public async Task Update(TrainingEvaluationType trainingEvaluationType)
        {
            if (trainingEvaluationType == null)
            {
                throw new ArgumentNullException(nameof(trainingEvaluationType));
            }

            _context.TrainingEvaluationTypes.Update(trainingEvaluationType);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var evaluationTypeToDelete = await FindById(id);
            if (evaluationTypeToDelete == null)
            {
                return false;
            }

            _context.TrainingEvaluationTypes.Remove(evaluationTypeToDelete);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
