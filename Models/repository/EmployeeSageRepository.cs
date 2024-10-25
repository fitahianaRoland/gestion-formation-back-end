using gestion_fomation_back_end_local.data;
using gestion_fomation_back_end_local.Models.models;
using Microsoft.EntityFrameworkCore;

namespace gestion_fomation_back_end_local.Models.repository
{
    public class EmployeeSageRepository
    {
        private readonly SageDbContext _context;
        private readonly ILogger<EmployeeSageRepository> _logger;

        public EmployeeSageRepository(SageDbContext context, ILogger<EmployeeSageRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        // CREATE
        public async Task<EmployeeSage> CreateEmployeeAsync(EmployeeSage employee)
        {
            try
            {
                await _context.AddAsync(employee);
                await _context.SaveChangesAsync();
                return employee;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating employee: {ex.Message}");
                throw;
            }
        }

        // READ (Get all employees)
        public async Task<List<EmployeeSage>> GetAllEmployeesAsync()
        {
            return await _context.Employee.ToListAsync();
        }

        // READ (Get employee by ID)
        public async Task<EmployeeSage?> GetEmployeeByIdAsync(int id)
        {
            return await _context.Employee.FindAsync(id);
        }

        // UPDATE
        public async Task<EmployeeSage?> UpdateEmployeeAsync(int id, Employee updatedEmployee)
        {
            var existingEmployee = await GetEmployeeByIdAsync(id);
            if (existingEmployee == null)
            {
                return null;
            }

            // Mise à jour des propriétés
            existingEmployee.RegistrationNumber = updatedEmployee.RegistrationNumber;
            existingEmployee.Name = updatedEmployee.Name;
            existingEmployee.FirstName = updatedEmployee.FirstName;
            existingEmployee.Email = updatedEmployee.Email;

            await _context.SaveChangesAsync();
            return existingEmployee;
        }

        // DELETE
        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employee = await GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return false;
            }

            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
