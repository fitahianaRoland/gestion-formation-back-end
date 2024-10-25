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

        // GET EMPLOYEES WITH DEPARTMENT WITH PAGINATION AND FILTER BY DEPARTMENT , NAME , FIRSTNAME
        public async Task<(List<EmployeeWithDepartment>, int)> GetEmployeesWithDepartments(int? departmentId = null, string? mot = null, int page = 1, int limit = 10)
        {
            int skip = (page - 1) * limit;

            // Construire la requête pour obtenir les employés avec leurs départements
            var query = from Employee in _context.Employee
                        join department in _context.Department on Employee.DepartmentId equals department.DepartmentId
                        where (!departmentId.HasValue || Employee.DepartmentId == departmentId.Value) &&
                              (string.IsNullOrEmpty(mot) || Employee.Name.Contains(mot) || Employee.FirstName.Contains(mot))
                        select new EmployeeWithDepartment
                        {
                            EmployeeId = Employee.EmployeeId,
                            DepartmentId = Employee.DepartmentId,
                            DepartmentName = department.DepartmentName,
                            RegistrationNumber = Employee.RegistrationNumber,
                            Name = Employee.Name,
                            FirstName = Employee.FirstName,
                            DateHire = Employee.DateHire,
                            Email = Employee.Email
                        };

            // Récupérer le total d'employés correspondant aux critères
            int totalEmployees = await query.CountAsync();

            // Appliquer la pagination
            var employeesWithDepartments = await query.Skip(skip).Take(limit).ToListAsync();

            return (employeesWithDepartments, totalEmployees);
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
