using GestionFormation.Models.classes;
using Microsoft.EntityFrameworkCore;

namespace GestionFormation.Models.repository
{
    public class TrainerTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public TrainerTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Trainer_Type>> FindAll()
        {
            return await _context.trainer_Types.ToListAsync() ?? new List<Trainer_Type>();
        }

        public async Task<Trainer_Type> FindById(int id)
        {
            return await _context.trainer_Types.FindAsync(id);
        }
    }
}
