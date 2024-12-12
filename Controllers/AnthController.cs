using Microsoft.AspNetCore.Mvc;
using GestionFormation.Models.repository;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using GestionFormation.Models.classes;
using Microsoft.Extensions.Configuration;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AppUserRepository _appuser;
    private readonly UserRoleRepository _userRoleRepository;
    private readonly IConfiguration _configuration;
    private readonly AdminRepository _adminRepository;
    public AuthController(AppUserRepository appuser, 
        UserRoleRepository userRoleRepository,
        IConfiguration configuration,
        AdminRepository adminRepository
        )
    {
        _appuser = appuser;
        _userRoleRepository = userRoleRepository;
        _configuration = configuration;
        _adminRepository = adminRepository;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(string email, string password)
    {
        var adminId = _appuser.Authenticate(email, password);

        if (adminId == null)
        {
            return Unauthorized("Nom d'utilisateur ou mot de passe incorrect.");
        }
        var user = await _appuser.GetUserById(adminId);
        var token = GenerateJwtToken(adminId);
        return Ok(new { 
            Token = token, 
            User = user
        });
    }

    private async Task<string> GenerateJwtToken(int? adminId)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("CeciEstUneCléSuperSécurisée1234!"));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub,adminId.ToString()), 
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        //List<UserRoleView> roles = await _userRoleRepository.GetUserRolesByUserId(adminId);
        //foreach (var role in roles)
        //{
        //    claims.Add(new Claim(ClaimTypes.Role, role.RoleDescription));
        //}
        AppUser user = await _appuser.GetUserById(adminId);
        ViewRoleProfile role = await _adminRepository.GetRoleByProfileId(user.ProfileId);
        List<ViewAccessProfile> accesses = await _adminRepository.GetAccessByProfileId(user.ProfileId);
        claims.Add(new Claim(ClaimTypes.Role, role.RoleDescription));
        if (role.RoleDescription == "Admin")
        {
            Console.WriteLine("connecté en tant qu'admin ! ");
            claims.Add(new Claim("Access", "Lire"));
            claims.Add(new Claim("Access", "Ecrire"));
            claims.Add(new Claim("Access", "Modifier"));
            claims.Add(new Claim("Access", "Supprimer"));
        }
        else
        {
            Console.WriteLine("connecté en tant qu'utilisateur normal ! ");
            foreach (var access in accesses)
            {
                Console.WriteLine($"{access.AccessDescription}");
                claims.Add(new Claim("Access", access.AccessDescription));
            }
        }
        Console.WriteLine("heho" + role.RoleDescription);

        var token = new JwtSecurityToken(
            issuer: "http://localhost:3002",
            audience: "http://localhost:3002",
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        Console.WriteLine("Déconnexion réussie");
        return Ok(new { message = "Déconnexion réussie." });
    }


}
