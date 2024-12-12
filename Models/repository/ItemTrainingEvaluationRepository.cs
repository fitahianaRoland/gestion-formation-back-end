using GestionFormation.Models.classes;
using Microsoft.EntityFrameworkCore;
using System;

namespace GestionFormation.Repository
{
    public class ItemTrainingEvaluationRepository
    {
        private readonly ApplicationDbContext _context;

        public ItemTrainingEvaluationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Add new ItemTrainingEvaluation
        public async Task<ItemTrainingEvaluation> AddAsync(ItemTrainingEvaluation item)
        {
            _context.itemTrainingEvaluation.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        // Get all items
        public async Task<List<ItemTrainingEvaluation>> GetAllAsync()
        {
            return await _context.itemTrainingEvaluation.ToListAsync();
        }


        public async Task AddByTrainingIdTrainingSessionIdAsync(int trainingId, int trainingSessionId, int[] trainingEvaluationTypeId)
        {
            if (trainingEvaluationTypeId == null || trainingEvaluationTypeId.Length == 0)
            {
                throw new ArgumentException("Le tableau des types d'évaluation ne peut pas être vide.", nameof(trainingEvaluationTypeId));
            }

            var newItems = trainingEvaluationTypeId.Select(typeId => new ItemTrainingEvaluation
            {
                TrainingId = trainingId,
                TrainingSessionId = trainingSessionId,
                TrainingEvaluationTypeId = typeId
            }).ToList();

            _context.itemTrainingEvaluation.AddRange(newItems);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ItemTrainingEvaluation>> GetByTrainingIdTrainingSessionIdAsync(int trainingId,int trainingSessionId)
        {
            return await _context.itemTrainingEvaluation.Where(e => e.TrainingId == trainingId && e.TrainingSessionId == trainingSessionId).ToListAsync();
        }

    }
}
