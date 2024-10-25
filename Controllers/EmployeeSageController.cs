using gestion_fomation_back_end_local.Models.models;
using gestion_fomation_back_end_local.Models.repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace gestion_fomation_back_end_local.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeSageController : ControllerBase
    {
        private readonly EmployeeSageRepository _employeeRepository;

        public EmployeeSageController(EmployeeSageRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeRepository.GetAllEmployeesAsync();
            return Ok(employees);
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }
    }
}
