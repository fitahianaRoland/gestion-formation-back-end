using GestionFormation.Models.classes;
using Microsoft.EntityFrameworkCore;

namespace GestionFormation.Repository
{
    public class SendingStatusRepository
    {
        private readonly ApplicationDbContext _context;

        public SendingStatusRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SendingStatus> AddAsync(SendingStatus status)
        {
            if (status == null)
                throw new ArgumentNullException(nameof(status));

            _context.sendingStatus.Add(status);
            await _context.SaveChangesAsync();
            return status;
        }

        public async Task<List<SendingStatus>> GetAllAsync()
        {
            return await _context.sendingStatus.ToListAsync();
        }

        public async Task<int> GetIdByValueAsync(int value)
        {
            var status = await _context.sendingStatus
                .Where(e => e.Value == value)
                .Select(e => (int)e.Id)
                .FirstOrDefaultAsync();
            return status;
        }
    }
}
