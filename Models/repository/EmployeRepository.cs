using GestionFormation.Models.classes;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace GestionFormation.Models.repository
{
    public class EmployeRepository
    {
        private readonly SimDbContext _contextsim;

        public EmployeRepository(SimDbContext context)
        {
            _contextsim = context;
        }

        public async Task<List<Employee>> FindAll()
        {
            return await _contextsim.employees.ToListAsync();
        }

        public async Task<Employee?> FindById(int id)
        {
            return await _contextsim.employees.FindAsync(id);
        }

        public async Task<List<Employee>> Search(string lettre)
        {
            if (string.IsNullOrWhiteSpace(lettre))
            {
                return new List<Employee>(); 
            }

            return await _contextsim.employees
                .Where(e => e.Name.StartsWith(lettre) || e.FirstName.StartsWith(lettre))
                .ToListAsync();
        }

        //public async Task<List<Employee>> FindByDeptID(int deptID)
        //{
        //    return await _context.employees.Where(t => t.department_id == deptID).ToListAsync();
        //}
    }
}
