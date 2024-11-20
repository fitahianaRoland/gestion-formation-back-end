using GestionFormation.Models.classes;
using Microsoft.EntityFrameworkCore;

namespace GestionFormation.Models.repository
{
    public class UserRoleRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserRoleRepository> _logger;

        public UserRoleRepository(ApplicationDbContext context, ILogger<UserRoleRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<UserRoleView>> FindAll()
        {
            return await _context.userRoleView.ToListAsync() ?? new List<UserRoleView>();
        }
        public async Task<List<UserRoleView>> GetUserRolesByUserId(int? userId)
        {
            string sql = "select * from view_user_role where user_id = {0}";
            return await _context.userRoleView.FromSqlRaw(sql, userId).ToListAsync();
        }   


    }
}
