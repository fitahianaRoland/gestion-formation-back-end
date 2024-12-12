using GestionFormation.Models.classes;
using GestionFormation.Models.Classes;
using Microsoft.EntityFrameworkCore;

namespace GestionFormation.Repository
{
    public class TrainingSessionStatusRepository
    {
        private readonly ApplicationDbContext _context;

        public TrainingSessionStatusRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TrainingSessionStatus> AddAsync(TrainingSessionStatus status)
        {
            if (status == null)
                throw new ArgumentNullException(nameof(status));

            _context.trainingSessionStatus.Add(status);
            await _context.SaveChangesAsync();
            return status;
        }

        public async Task<List<TrainingSessionStatus>> GetAllAsync()
        {
            return await _context.trainingSessionStatus.ToListAsync();
        }

        public async Task<int> GetIdByValueAsync(int value)
        {
            var status = await _context.trainingSessionStatus
                .Where(e => e.Value == value)
                .Select(e => (int)e.Id)
                .FirstOrDefaultAsync();
            return status;
        }
    }
}
