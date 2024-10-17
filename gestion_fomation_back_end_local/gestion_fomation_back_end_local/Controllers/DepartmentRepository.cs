using gestion_fomation_back_end_local.Models.models;
using gestion_fomation_back_end_local.Models.repository;
using Microsoft.AspNetCore.Mvc;

namespace gestion_fomation_back_end_local.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly DepartmentRepository _departmentRepository;

        public DepartmentController(DepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        // POST: api/Department
        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] Department department)
        {
            if (department == null)
            {
                return BadRequest("Department is null.");
            }

            var createdDepartment = await _departmentRepository.CreateDepartmentAsync(department);
            return CreatedAtAction(nameof(GetDepartmentById), new { id = createdDepartment.DepartmentId }, createdDepartment);
        }

        // GET: api/Department
        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            var departments = await _departmentRepository.GetAllDepartmentsAsync();
            return Ok(departments);
        }

        // GET: api/Department/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var department = await _departmentRepository.GetDepartmentByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }

        // PUT: api/Department/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] Department updatedDepartment)
        {
            if (updatedDepartment == null)
            {
                return BadRequest("Department is null.");
            }

            var department = await _departmentRepository.UpdateDepartmentAsync(id, updatedDepartment);
            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }

        // DELETE: api/Department/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var result = await _departmentRepository.DeleteDepartmentAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
