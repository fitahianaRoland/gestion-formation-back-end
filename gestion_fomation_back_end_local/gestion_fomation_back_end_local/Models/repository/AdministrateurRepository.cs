using gestion_fomation_back_end_local.data;
using gestion_fomation_back_end_local.Models.models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace gestion_fomation_back_end_local.Models.repository
{
    public class AdministrateurRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AdministrateurRepository> _logger;
        public AdministrateurRepository(ApplicationDbContext context, ILogger<AdministrateurRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Administrateur> ValidateUserAsync(string? email, string? motDePasse)
        {
            Administrateur? user = null;
            try
            {
                user = await _context.administrateur
                    .FirstOrDefaultAsync(a => a.Email == email && a.Mot_De_Passe == motDePasse);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
            }
            return user; // Retourne l'utilisateur s'il existe, sinon null.
        }
    }
}
