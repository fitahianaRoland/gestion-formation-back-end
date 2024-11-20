using Microsoft.AspNetCore.Mvc;
using GestionFormation.Models.repository;
using Microsoft.AspNetCore.Authorization;
using GestionFormation.Models.classes;

[Route("api/[controller]")]
[ApiController]
//[Authorize] 
public class HomeController : ControllerBase
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;
    private readonly DepartementRepository _departementRepository;
    private readonly TrainerTypeRepository _trainertypeRepository;
    private readonly EmployeRepository _employeeRepository;

    public HomeController(
        ApplicationDbContext context, 
        ILogger<HomeController> logger, 
        DepartementRepository dept,
        TrainerTypeRepository trainerType,
        EmployeRepository  employeRepository
     )
    {
        _context = context;
        _logger = logger;
        _departementRepository = dept;
        _trainertypeRepository = trainerType;
        _employeeRepository = employeRepository;
    }

    [HttpGet("employe")]
    public async Task<IActionResult> GetAllEmployees()
    {
        var employees = await _employeeRepository.FindAll();
        return Ok(employees);
    }

    [HttpGet("GetEmployeesLimited")]
    public async Task<IActionResult> GetEmployeesLimited(string name = null, int page = 1, int limit = 10)
    {
        var (employeesWithDepartments, totalEmployees) = await _employeeRepository.GetEmployeesLimited(name,page,limit);
        return Ok(new
        {
            Total = totalEmployees,
            Employees = employeesWithDepartments
        });
    }

    [HttpGet("employe/{id}")]
    public async Task<IActionResult> GetEmployeeById(int id)
    {
        var employee = await _employeeRepository.FindById(id);
        if (employee == null)
        {
            return NotFound();
        }
        return Ok(employee);
    }

    [HttpGet("trainerType")]
    public async Task<IActionResult> GetAllTrainerType()
    {
        var ttype = await _trainertypeRepository.FindAll();
        return Ok(ttype);
    }

    [HttpGet("department")]
    public async Task<IActionResult> GetAllDept()
    {
        var dept = await _departementRepository.FindAll();
        return Ok(dept);
    }

    [HttpGet("department/{id}")]
    public async Task<IActionResult> GetDepartmentById(int id)
    {
        var department = await _departementRepository.FindById(id);
        if (department == null)
        {
            return NotFound();
        }

        return Ok(department);
    }


}
