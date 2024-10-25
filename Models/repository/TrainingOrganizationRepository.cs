using GestionFormation.Models.classes;
using Microsoft.EntityFrameworkCore;

namespace GestionFormation.Models.repository
{
    public class TrainingOrganizationRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TrainingOrganizationRepository> _logger;

        public TrainingOrganizationRepository(ApplicationDbContext context, ILogger<TrainingOrganizationRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<TrainingOrganization>> FindAll()
        {
            return await _context.trainingOrganizations?.ToListAsync() ?? new List<TrainingOrganization>();
        }

        public async Task<TrainingOrganization> FindById(int id)
        {
            return await _context.trainingOrganizations.FindAsync(id);
        }

        //creat trainingOrganization
        public async Task<TrainingOrganization> Add(TrainingOrganization trainingOrganization)
        {
            try
            {
                await _context.AddAsync(trainingOrganization);
                await _context.SaveChangesAsync();
                return trainingOrganization;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating training: {ex.Message}");
                throw;
            }
        }


        public async Task<TrainingOrganization?> Update(TrainingOrganization trainingOrganization)
        {
            if (trainingOrganization == null)
            {
                throw new ArgumentNullException(nameof(trainingOrganization));
            }

            _context.trainingOrganizations.Update(trainingOrganization);
            await _context.SaveChangesAsync();
            return trainingOrganization;
        }

        public async Task<bool> Delete(int id)
        {
            var trainingToDelete = await FindById(id);
            if (trainingToDelete == null)
            {
                return false;
            }

            _context.trainingOrganizations.Remove(trainingToDelete);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
