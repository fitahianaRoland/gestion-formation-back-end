using GestionFormation.Models.classes;
using Microsoft.EntityFrameworkCore;

namespace GestionFormation.Models.repository
{
    public class TrainerOrganizationRepository
    {
        private readonly ApplicationDbContext _context;

        public TrainerOrganizationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TrainerOrganization>> FindAll()
        {
            return await _context.trainerOrganizations.ToListAsync() ?? new List<TrainerOrganization>();
        }

        public async Task Add(TrainerOrganization trainer)
        {
            if (trainer == null)
            {
                throw new ArgumentNullException(nameof(trainer));
            }

            await _context.trainerOrganizations.AddAsync(trainer);
            await _context.SaveChangesAsync();
        }
    }
}
