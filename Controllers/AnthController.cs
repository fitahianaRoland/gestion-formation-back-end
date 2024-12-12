using Microsoft.AspNetCore.Mvc;
using GestionFormation.Models.repository;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AppUserRepository _appuser;

    public AuthController(AppUserRepository admn)
    {
        _appuser = admn;
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
        
    private string GenerateJwtToken(int? adminId)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("CeciEstUneCléSuperSécurisée1234!"));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub,adminId.ToString()), 
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: "http://localhost:3002",
            audience: "http://localhost:3002",
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
