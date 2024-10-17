using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

using gestion_fomation_back_end_local.Models.models;
using gestion_fomation_back_end_local.Models.repository;

namespace gestion_fomation_back_end_local.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private AdministrateurRepository _administrateurRepository;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IConfiguration configuration, AdministrateurRepository administrateurRepository, ILogger<AuthController> logger)
        { 
            _configuration = configuration;
            _administrateurRepository = administrateurRepository;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Administrateur admin)
        {
            if (admin == null)
            {
                return BadRequest("Admin object is null");
            }
            var administrateur = await _administrateurRepository.ValidateUserAsync(admin.Email, admin.Mot_De_Passe);
            //_logger.LogInformation("***************** email : "+administrateur.Email);
            if (administrateur != null)
            {
            _logger.LogInformation("test test fitahiana");
                var token = new Token().GenerateJwtToken(_configuration,admin.Email);
                return Ok(new { token });
            }
            _logger.LogInformation("401 401 401 !!!");
            return Unauthorized();
        }

    }
}
