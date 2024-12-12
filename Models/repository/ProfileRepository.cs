using GestionFormation.Models.classes;
using Microsoft.EntityFrameworkCore;

namespace GestionFormation.Models.repository
{
    public class ProfileRepository
    {
        private readonly ApplicationDbContext _context;

        public ProfileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Profile>> FindAll()
        {
            return await _context.profiles.ToListAsync() ?? new List<Profile>();
        }

        public async Task Add(Profile newProfile)
        {
            if (newProfile == null)
            {
                throw new ArgumentNullException(nameof(newProfile));
            }

            await _context.profiles.AddAsync(newProfile);
            await _context.SaveChangesAsync();
        }

    }
}
