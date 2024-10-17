using gestion_fomation_back_end_local.data;
using gestion_fomation_back_end_local.Models.models;
using Microsoft.EntityFrameworkCore;

namespace gestion_fomation_back_end_local.Models.repository
{
    public class EmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EmployeeRepository> _logger;

        public EmployeeRepository(ApplicationDbContext context, ILogger<EmployeeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        // CREATE
        public async Task<Employee> CreateEmployeeAsync(Employee employee)
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
        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            return await _context.Employee.ToListAsync();
        }

        // READ (Get employee by ID)
        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            return await _context.Employee.FindAsync(id);
        }

        // UPDATE
        public async Task<Employee?> UpdateEmployeeAsync(int id, Employee updatedEmployee)
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
            existingEmployee.DateHire = updatedEmployee.DateHire;
            existingEmployee.DepartmentId = updatedEmployee.DepartmentId;
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
