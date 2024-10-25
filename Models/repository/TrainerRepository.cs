using GestionFormation.Models.classes;
using Microsoft.EntityFrameworkCore;

namespace GestionFormation.Models.repository
{
    public class TrainerRepository
    {
        private readonly ApplicationDbContext _context;

        public TrainerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Trainer>> FindAll()
        {
            return await _context.Trainers?.ToListAsync() ?? new List<Trainer>();
        }

        public async Task<Trainer> FindById(int id)
        {
            return await _context.Trainers.FindAsync(id);
        }

        public async Task Add(Trainer trainer)
        {
            if (trainer == null)
            {
                throw new ArgumentNullException(nameof(trainer));
            }

            await _context.Trainers.AddAsync(trainer);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Trainer trainer)
        {
            if (trainer == null)
            {
                throw new ArgumentNullException(nameof(trainer));
            }

            _context.Trainers.Update(trainer);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var trainerToDelete = await FindById(id);
            if (trainerToDelete == null)
            {
                return false;
            }

            _context.Trainers.Remove(trainerToDelete);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
