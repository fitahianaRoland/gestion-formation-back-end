using GestionFormation.Models.classes;
using Microsoft.EntityFrameworkCore;

namespace GestionFormation.Models.repository
{
    public class DepartementRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartementRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Departement>> FindAll()
        {
            return await _context.Departements.ToListAsync() ?? new List<Departement>();
        }

        public async Task<Departement> FindById(int id)
        {
            return await _context.Departements.FindAsync(id);
        }
    }
}
