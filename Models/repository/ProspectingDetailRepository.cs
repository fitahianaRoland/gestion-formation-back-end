using GestionFormation.Models.classes;
using Microsoft.EntityFrameworkCore;

namespace GestionFormation.Models.repository
{
    public class ProspectingDetailRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SessionRepository> _logger;

        public ProspectingDetailRepository(ApplicationDbContext context, ILogger<SessionRepository> logger)
        {
            _context = context; 
            _logger = logger;
        }
        
        public async Task<List<ProspectingDetail>> FindAll()
        {
            return await _context.prospectingDetails.ToListAsync() ?? new List<ProspectingDetail>();
        }

        public async Task<ProspectingDetail> Add(ProspectingDetail prospecting)
        {
            try
            {
                await _context.AddAsync(prospecting);
                await _context.SaveChangesAsync();
                return prospecting;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating Prospeting session: {ex.Message}");
                throw;
            }
        }
    }
}
