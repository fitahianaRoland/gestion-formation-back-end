using GestionFormation.Models.classes;
using Microsoft.EntityFrameworkCore;

namespace GestionFormation.Models.repository
{
    public class DifferentCostRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SessionRepository> _logger;
        public DifferentCostRepository(ApplicationDbContext context, ILogger<SessionRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<DifferentCost>> FindAll()
        {
            return await _context.differentCosts.ToListAsync() ?? new List<DifferentCost>();
        }

        public async Task<DifferentCost?> FindById(int id)
        {
            return await _context.differentCosts.FindAsync(id); 
        }
    }
}
