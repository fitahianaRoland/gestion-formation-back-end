using GestionFormation.Models.classes;
using Microsoft.EntityFrameworkCore;

namespace GestionFormation.Models.repository
{
    public class AdminRepository
    {
        private readonly ApplicationDbContext _context;

        public AdminRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Admin> FindAll()
        {
            return _context.Admins?.ToList() ?? new List<Admin>();
        }

        public int? Authenticate(string nom, string password)
        {
            var selectAll = this.FindAll();
            foreach (var admin in selectAll)
            {
                if (admin.Nom == nom && admin.Password == password)
                {
                    return admin.Id;
                }
            }
            return null;
        }
    }
}
