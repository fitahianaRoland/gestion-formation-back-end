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

        public async Task<List<Department>> FindAll()
        {
            return await _context.Departements.ToListAsync() ?? new List<Department>();
        }

        public async Task<Department> FindById(int id)
        {
            return await _context.Departements.FindAsync(id);
        }
    }
}
