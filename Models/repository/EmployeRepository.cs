using GestionFormation.Models.classes;
using Microsoft.EntityFrameworkCore;

namespace GestionFormation.Models.repository
{
    public class EmployeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> FindAll()
        {
            return await _context.employees.ToListAsync();
        }

        public async Task<Employee?> FindById(int id)
        {
            return await _context.employees.FindAsync(id);
        }
    }
}
