using GestionFormation.Models.classes;
using Microsoft.EntityFrameworkCore;

namespace GestionFormation.Models.repository
{
    public class CategorieRequestRepository
    {
        private readonly ApplicationDbContext _context;

        public CategorieRequestRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<List<CategoriesRequest>> FindAll()
        {
            return await _context.categories.ToListAsync() ?? new List<CategoriesRequest>();
        }

    }
}
