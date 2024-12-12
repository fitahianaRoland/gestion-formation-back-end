using GestionFormation.Models.classes;
using Microsoft.EntityFrameworkCore;

namespace GestionFormation.Models.repository
{
    public class AppUserRepository
    {
        private readonly ApplicationDbContext _context;

        public AppUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<AppUser> FindAll()
        {
            return _context.AppUser?.ToList() ?? new List<AppUser>();
        }

        public int? Authenticate(string email, string password)
        {
            var selectAll = this.FindAll();
            foreach (var appuser in selectAll)
            {
                if (appuser.Email == email && appuser.Password == password)
                {
                    return appuser.Id;
                }
            }
            return null;
        }
    }
}
