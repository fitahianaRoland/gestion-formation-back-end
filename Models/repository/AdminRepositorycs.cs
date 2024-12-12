using GestionFormation.Models.classes;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Tls;

namespace GestionFormation.Models.repository
{
    public class AdminRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ViewAccessProfile _viewAccessProfile;
        private readonly ViewRoleProfile _viewRoleProfile;
        private readonly AppUserRepository _appUserRepository;
        public AdminRepository(ApplicationDbContext context, ViewAccessProfile viewAccessProfile, ViewRoleProfile viewRoleProfile, AppUserRepository appUserRepository)
        {
            _context = context;
            _viewAccessProfile = viewAccessProfile;
            _viewRoleProfile = viewRoleProfile;
            _appUserRepository = appUserRepository;
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

        public async Task<ViewRoleProfile> GetRoleByProfileId(int profile_id)
        {
            string sql = "select * from view_role_profile where profile_id = {0}";
            return await _context.viewRoleProfiles.FromSqlRaw(sql, profile_id).FirstOrDefaultAsync();
        }

        public async Task<List<ViewAccessProfile>> GetAccessByProfileId(int profile_id)
        {
            string sql = "select * from view_access_profile where profile_id = {0}";
            return await _context.viewAccessProfiles.FromSqlRaw(sql, profile_id).ToListAsync();
        }

        public async Task AddRoleProfile(RoleProfile newRoleProfile)
        {
            try
            {
                if (newRoleProfile == null)
                {
                    throw new ArgumentNullException(nameof(newRoleProfile));
                }
                await _context.roleProfiles.AddAsync(newRoleProfile);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is Microsoft.Data.SqlClient.SqlException sqlEx)
                {
                    if (sqlEx.Number == 2627 || sqlEx.Number == 2601)
                    {
                        Console.WriteLine("Violation de contrainte UNIQUE, entrée ignorée.");
                        throw new ArgumentException("La relation entre le rôle et le profil existe déjà.");
                    }
                }
                throw;
            }
        }

        public async Task AddAccessProfile(AccessProfile newAccessProfile)
        {
            try
            {
                if (newAccessProfile == null)
                {
                    throw new ArgumentNullException(nameof(newAccessProfile));
                }
                await _context.accessProfiles.AddAsync(newAccessProfile);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is Microsoft.Data.SqlClient.SqlException sqlEx)
                {
                    if (sqlEx.Number == 2627 || sqlEx.Number == 2601)
                    {
                        Console.WriteLine("Violation de contrainte UNIQUE, entrée ignorée.");
                        throw new ArgumentException("La relation entre l'accès et le profil existe déjà.");
                    }
                }
                throw;
            }
        }
        public async Task<List<Role>> GetAllRole()
        {
            return await _context.roles.ToListAsync() ?? new List<Role>();
        }
        public async Task<List<Access>> GetAllAccess()
        {
            return await _context.accesses.ToListAsync() ?? new List<Access>();
        }
        public async Task<List<Profile>> GetAllProfile()
        {
            return await _context.profiles.ToListAsync() ?? new List<Profile>();
        }

        public async Task<List<ViewAccessProfile>> GetAllAccessProfile()
        {
            return await _context.viewAccessProfiles.ToListAsync();
        }
        public async Task<List<ViewAccessProfile>> GetAccessByProfile(int profile_id)
        {
            return await _context.viewAccessProfiles.Where(e => e.ProfileId == profile_id).ToListAsync();
        }



    }
}
