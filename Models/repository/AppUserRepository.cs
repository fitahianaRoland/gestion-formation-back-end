using GestionFormation.Models.classes;
using Microsoft.Data.SqlClient;
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

        public async Task<AppUser> GetUserById(int? user_id)
        {
            AppUser user= await _context.AppUser.Include(e => e.profile).Where(e => e.Id == user_id).FirstOrDefaultAsync() ?? new AppUser();
            return user;
        }

        public async Task<List<AppUser>> GetAllUser()
        {
            return await _context.AppUser.Include(e => e.profile).ToListAsync() ?? new List<AppUser>();
        }

        public async Task<(List<AppUser>, int)> GetUserLimited(string? name, int page, int limit)
        {
            int Limit = limit;
            int skip = (page - 1) * Limit;
            // Récupérer le total d'employés correspondant aux critères
            var query = _context.AppUser.AsQueryable();
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.Name.Contains(name) || e.FirstName.Contains(name));
            }
            // Calculer le total d'employés correspondant aux critères 
            var totalEmployees = await query.CountAsync();
            // Appliquer la pagination seulement si 'name' est null ou vide
            List<AppUser> users;
            if (string.IsNullOrEmpty(name))
            {
                users = await query.Skip(skip).Take(limit).Include(e => e.profile).ToListAsync();
            }
            else
            {
                users = await query.Include(e => e.profile).ToListAsync();
            }
            return (users, totalEmployees);
        }

        public async Task AddUser(AppUser app_user)
        {
            if (app_user == null) { throw new ArgumentException(" utiliisateur invalide ! "); }
            try
            {
                await _context.AddAsync(app_user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                if (dbEx.InnerException is SqlException sqlEx && sqlEx.Number == 2627)
                {
                    throw new Exception($"L'email {app_user.Email} est déjà utilisé.");
                }
                throw new Exception("Erreur lors de l'ajout de l'utilisateur : " + dbEx.Message, dbEx);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(" message d'erreur : "+ex.Message);
            }
        }

        public async Task UpdateUser(AppUser app_user)
        {
            if (app_user == null || app_user.Id <= 0){throw new ArgumentException("Utilisateur invalide !");}
            try
            {
                var existingUser = await _context.AppUser.FindAsync(app_user.Id);
                if (existingUser == null){throw new Exception("Utilisateur non trouvé !");}
                existingUser.Name = app_user.Name;
                existingUser.FirstName = app_user.FirstName;
                existingUser.Password = app_user.Password;
                existingUser.Email = app_user.Email;
                existingUser.ProfileId = app_user.ProfileId;
                _context.AppUser.Update(existingUser);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                throw new Exception("Message d'erreur : " + ex.Message);
            }
        }

    }
}
