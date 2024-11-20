using Microsoft.AspNetCore.Mvc;
using GestionFormation.Models.repository;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using GestionFormation.Models.classes;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AppUserRepository _appuser;
    private readonly UserRoleRepository _userRoleRepository;

    public AuthController(AppUserRepository appuser, UserRoleRepository userRoleRepository)
    {
        _appuser = appuser;
        _userRoleRepository = userRoleRepository;
    }

    [HttpPost("login")]
    public IActionResult Login(string email, string password)
    {
        var adminId = _appuser.Authenticate(email, password);

        if (adminId == null)
        {
            return Unauthorized("Nom d'utilisateur ou mot de passe incorrect.");
        }

        var token = GenerateJwtToken(adminId);
        return Ok(new { Token = token });
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
        List<UserRoleView> roles = await _userRoleRepository.GetUserRolesByUserId(adminId);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role.RoleDescription));
        }

        var token = new JwtSecurityToken(
            issuer: "http://localhost:3002",
            audience: "http://localhost:3002",
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
