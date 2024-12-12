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

            public async Task<(List<Employee>, int)> GetEmployeesLimited(string? name, int page, int limit)
            {
                int Limit = limit;
                int skip = (page - 1) * Limit;

                // Récupérer le total d'employés correspondant aux critères
                var query = _context.employees.AsQueryable();

                if (!string.IsNullOrEmpty(name))
                {
                    query = query.Where(e => e.Name.Contains(name) || e.FirstName.Contains(name));
                }

            // Calculer le total d'employés correspondant aux critères 
                var totalEmployees = await query.CountAsync();

                // Appliquer la pagination seulement si 'name' est null ou vide
                List<Employee> employees;
                if (string.IsNullOrEmpty(name))
                {
                    employees = await query.Skip(skip).Take(limit).ToListAsync();
                }
                else
                {
                    employees = await query.ToListAsync();
                }
                return (employees, totalEmployees);
            }


    }
}
