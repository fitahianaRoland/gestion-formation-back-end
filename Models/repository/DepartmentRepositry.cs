using gestion_fomation_back_end_local.data;
using gestion_fomation_back_end_local.Models.models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace gestion_fomation_back_end_local.Models.repository
{
    public class DepartmentRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DepartmentRepository> _logger;

        public DepartmentRepository(ApplicationDbContext context, ILogger<DepartmentRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        // CREATE
        public async Task<Department> CreateDepartmentAsync(Department department)
        {
            try
            {
                await _context.AddAsync(department);
                await _context.SaveChangesAsync();
                return department;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating department: {ex.Message}");
                throw;
            }
        }

        // READ (Get all departments)
        public async Task<List<Department>> GetAllDepartmentsAsync()
        {
            return await _context.Department.ToListAsync();
        }

        // READ (Get department by ID)
        public async Task<Department?> GetDepartmentByIdAsync(int id)
        {
            return await _context.Department.FindAsync(id);
        }

        // UPDATE
        public async Task<Department?> UpdateDepartmentAsync(int id, Department updatedDepartment)
        {
            var existingDepartment = await GetDepartmentByIdAsync(id);
            if (existingDepartment == null)
            {
                return null;
            }

            existingDepartment.DepartmentName = updatedDepartment.DepartmentName;

            await _context.SaveChangesAsync();
            return existingDepartment;
        }

        // DELETE
        public async Task<bool> DeleteDepartmentAsync(int id)
        {
            var department = await GetDepartmentByIdAsync(id);
            if (department == null)
            {
                return false;
            }

            _context.Department.Remove(department);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
