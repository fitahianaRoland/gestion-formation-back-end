using GestionFormation.Models.classes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GestionFormation.Models.repository
{
    public class InternalTrainingRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<InternalTrainingRepository> _logger;

        public InternalTrainingRepository(ApplicationDbContext context, ILogger<InternalTrainingRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<InternalTraining>> FindAll()
        {
            return await _context.interne.ToListAsync() ?? new List<InternalTraining>();
        }


        public async Task<InternalTraining> Add(InternalTraining interne)
        {
            try
            {
                await _context.AddAsync(interne);
                await _context.SaveChangesAsync();
                return interne;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating training session: {ex.Message}");
                throw;
            }
        }

        public async Task<InternalTraining?> FindById(int id)
        {
            return await _context.interne.FindAsync(id);
        }

        /*public async Task<InternalTraining?> UpdateState(int id, int state)
        {
            var interne = await FindById(id);
            if (interne == null)
            {
                return null;
            }

            interne.Validation = state;

            await _context.SaveChangesAsync();
            return interne;
        }
        */
    }
}
