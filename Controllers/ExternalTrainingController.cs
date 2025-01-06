using GestionFormation.Models.classes;
using GestionFormation.Models.repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionFormation.Controllers
{
    public class ExternalTrainingController : Controller
    {
        private readonly ILogger<ExternalTrainingController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly ExternalTrainingRepository _external;


        public ExternalTrainingController(
            ApplicationDbContext context,
            ILogger<ExternalTrainingController> logger,
            ExternalTrainingRepository ex
         )
        {
            _context = context;
            _logger = logger;
            _external = ex;
        }

        [HttpPost("addexternaltraining")]
        public async Task<IActionResult> AddTSession([FromBody] List<ExternalTraining> externals)
        {
            foreach (var external in externals)
            {
                var ex = new ExternalTraining()
                {
                   TrainingId = external.TrainingId,
                   TrainerOrganizationId = external.TrainerOrganizationId,
                   CategoriesId = external.CategoriesId,
                   Name = external.Name,
                   FirstName = external.FirstName,
                   PhoneNumber = external.PhoneNumber,
                   Email = external.Email,  
                   ValidationId = external.ValidationId,
                };

                await _external.Add(ex);
            }

            return Ok();
        }
    }
}
