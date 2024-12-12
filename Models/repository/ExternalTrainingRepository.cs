using GestionFormation.Models.classes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GestionFormation.Models.repository
{
    public class ExternalTrainingRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ExternalTrainingRepository> _logger;

        public ExternalTrainingRepository(ApplicationDbContext context,ILogger<ExternalTrainingRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<ExternalTraining>> FindAll()
        {
            return await _context.externe.ToListAsync() ?? new List<ExternalTraining>();
        }


        public async Task<ExternalTraining> Add(ExternalTraining externe)
        {
            try
            {
                await _context.AddAsync(externe);
                await _context.SaveChangesAsync();
                return externe;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating training session: {ex.Message}");
                throw;
            }
        }

    }
}
