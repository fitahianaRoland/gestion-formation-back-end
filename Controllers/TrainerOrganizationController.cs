using GestionFormation.Models.classes;
using GestionFormation.Models.repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TrainerOrganizationController : ControllerBase
    {

        private readonly ILogger<TrainerOrganizationController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly TrainerOrganizationRepository trainerOrganization;


        public TrainerOrganizationController(
            ApplicationDbContext context,
            ILogger<TrainerOrganizationController> logger,
            TrainerOrganizationRepository trainer 
         )
        {
            _context = context;
            _logger = logger;
            trainerOrganization = trainer;
        }

        [HttpPost("addTrainerOrganization")]
        public async Task<IActionResult> CreateTrainerOrganization([FromBody] TrainerOrganization request)
        {
            var trainer = new TrainerOrganization()
            {
                TrainerOrganizationName = request.TrainerOrganizationName,
            };

            await trainerOrganization.Add(trainer);

            return Ok(trainer);
        }

        [HttpGet("alltrainerorganization")]
        public async Task<IActionResult> GetAllTrainerOrganization()
        {
            var trainer = await trainerOrganization.FindAll();

            return Ok(trainer); ;
        }
    }

