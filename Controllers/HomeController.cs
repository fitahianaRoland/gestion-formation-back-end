using Microsoft.AspNetCore.Mvc;
using GestionFormation.Models.repository;
using Microsoft.AspNetCore.Authorization;
using GestionFormation.Models.classes;

[Route("api/[controller]")]
[ApiController]
[Authorize] 
public class HomeController : ControllerBase
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;
    private readonly DepartementRepository _departementRepository;
    private readonly TrainerTypeRepository _trainertypeRepository;
    private readonly EmployeRepository _employeeRepository;
    private readonly StateRepository _stateRepository;

    public HomeController(
        ApplicationDbContext context, 
        ILogger<HomeController> logger, 
        DepartementRepository dept,
        TrainerTypeRepository trainerType,
        EmployeRepository  employeRepository,
        StateRepository stateRepository
     )
    {
        _context = context;
        _logger = logger;
        _departementRepository = dept;
        _trainertypeRepository = trainerType;
        _employeeRepository = employeRepository;
        _stateRepository = stateRepository; 
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search(string lettre)
    {
        var results = await _employeeRepository.Search(lettre);

        if (results.Count == 0)
        {
            return NotFound("Aucun employé trouvé.");
        }

        return Ok(results);
    }


    [HttpGet("employe")]
    public async Task<IActionResult> GetAllEmployees()
    {
        var employees = await _employeeRepository.FindAll();
        return Ok(employees);
    }

    //[HttpGet("employe/getbydeptid")]
    //public async Task<IActionResult> GetEmployeeByDeptId(int deptid)
    //{
    //    var employee = await _employeeRepository.FindByDeptID(deptid);
    //    if (employee == null)
    //    {
    //        return NotFound();
    //    }
    //    return Ok(employee);
    //}

    [HttpGet("employe/getbyid")]
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

    [HttpGet("departement")]
    public async Task<IActionResult> GetAllDept()
    {
        var dept = await _departementRepository.FindAll();
        return Ok(dept);
    }

    [HttpGet("departement/{id}")]
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
