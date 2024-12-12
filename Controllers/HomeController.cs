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
    private readonly ProfileRepository _profileRepository;
    private readonly AdminRepository _adminRepository;
    public readonly AppUserRepository _appUserRepository;

    public HomeController(
        ApplicationDbContext context, 
        ILogger<HomeController> logger, 
        DepartementRepository dept,
        TrainerTypeRepository trainerType,
        EmployeRepository  employeRepository,
        ProfileRepository profileRepository,
        AdminRepository adminRepository,
        AppUserRepository appUserRepository
     )
    {
        _context = context;
        _logger = logger;
        _departementRepository = dept;
        _trainertypeRepository = trainerType;
        _employeeRepository = employeRepository;
        _profileRepository = profileRepository;
        _adminRepository = adminRepository;
        _appUserRepository = appUserRepository;
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

    [HttpPost("profile")]
    public async Task<IActionResult> AddNewProfile([FromBody] string description)
    {
        if (string.IsNullOrWhiteSpace(description) || description.Length == 0)
        {
            return BadRequest("La description ne peut pas être vide.");
        }
        Profile newProfile = new Profile
        {
            Description = description
        };
        await _profileRepository.Add(newProfile);
        return Ok("insertion réussi !");
    }

    [HttpPost("AddNewRoleProfile/{role_id}/{profile_id}")]
    public async Task<IActionResult> AddNewRoleProfile(int role_id, int profile_id)
    {
        try
        {
            RoleProfile newRoleProfile = new RoleProfile
            {
                RoleId = role_id,
                ProfileId = profile_id
            };
            await _adminRepository.AddRoleProfile(newRoleProfile);
            return Ok("insertion réussi !");
        }
        catch(ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("AddNewAccessProfile/{profile_id}/{access_id}")]
    public async Task<IActionResult> AddNewAccessProfile(int profile_id, int access_id)
    {
        try
        {
            AccessProfile newAccessProfile = new AccessProfile
            {
                ProfileId = profile_id,
                AccessId = access_id
            };
            await _adminRepository.AddAccessProfile(newAccessProfile);
            return Ok("insertion réussi !");
        }
        catch(ArgumentException ex)
        {
            return BadRequest(new {message = ex.Message});
        }
    }

    [HttpGet("GetAllRole")]
    public async Task<IActionResult> GetAllRole()
    {
        List<Role> listRole = await _adminRepository.GetAllRole();
        return Ok(listRole);
    }

    [HttpGet("GetAllProfile")]
    public async Task<IActionResult> GetAllProfile()
    {
        List<Profile> listProfile = await _adminRepository.GetAllProfile();
        return Ok(listProfile);
    }

    [HttpGet("GetAllAccess")]
    public async Task<IActionResult> GetAllAccess()
    {
        var listAccess = await _adminRepository.GetAllAccess();
        return Ok(listAccess);
    }

    [HttpGet("GetAccessByProfile/{profile_id}")]
    public async Task<IActionResult> GetAccessByProfile(int profile_id)
    {
        var listAccess = await _adminRepository.GetAccessByProfile(profile_id);
        return Ok(listAccess);
    }

    [HttpGet("GetAllAccessProfile")]
    public async Task<IActionResult> GetAllAccessProfile()
    {
        var listAccess = await _adminRepository.GetAllAccessProfile();
        return Ok(listAccess);
    }

    [HttpPost("AddUser")]
    public async Task<IActionResult> AddUser([FromBody] AppUser user_data)
    {
        try
        {
            await _appUserRepository.AddUser(user_data);
            return Ok("insertion utilisateur réussi ");
        }
        catch (Exception ex) { 
            return BadRequest(new { message = ex.Message });
        }
    }
    [HttpPut("UpdateUser")]
    public async Task<IActionResult> UpdateUser([FromBody] AppUser user_data)
    {
        try
        {
            await _appUserRepository.UpdateUser(user_data);
            return Ok("modification utilisateur réussi !");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("GetAllUser")]
    public async Task<IActionResult> GetAllUser()
    {
        var listAccess = await _appUserRepository.GetAllUser();
        return Ok(listAccess);
    }

    [HttpGet("GetUserLimited")]
    public async Task<IActionResult> GetUserLimited(string name = null, int page = 1, int limit = 10)
    {
        var (userList, total) = await _appUserRepository.GetUserLimited(name, page, limit);
        return Ok(new
        {
            Total = total,
            UserList = userList
        });
    }

}
