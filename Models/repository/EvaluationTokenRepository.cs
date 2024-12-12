using GestionFormation.Models.classes;
using Microsoft.EntityFrameworkCore;

namespace GestionFormation.Models.Repository
{
    public class EvaluationTokenRepository
    {
        private readonly ApplicationDbContext _context;

        public EvaluationTokenRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Méthode pour ajouter un nouvel EvaluationToken
        public async Task AddAsync(EvaluationToken evaluationToken)
        {
            if (evaluationToken == null)
                throw new ArgumentNullException(nameof(evaluationToken));

            await _context.evaluationToken.AddAsync(evaluationToken); // Attendez cette opération
            await _context.SaveChangesAsync(); // Et celle-ci aussi
        }

        // Méthode pour mettre à jour le champ IsUsed d'un EvaluationToken
        public void UpdateIsUsed(int id, bool isUsed)
        {
            var token = _context.evaluationToken.FirstOrDefault(e => e.Id == id);
            if (token == null)
                throw new ArgumentException("Token non trouvé.");

            token.IsUsed = isUsed;
        }
        public void UpdateSessionIdNotNull(int id, string sessionId)
        {
            var token = _context.evaluationToken.FirstOrDefault(e => e.Id == id);
            if (token == null)
                throw new ArgumentException("Token non trouvé.");

            token.SessionId = sessionId;
            //_context.SaveChangesAsync();
        }
        public void UpdateSessionIdNull(int id)
        {
            var token = _context.evaluationToken.FirstOrDefault(e => e.Id == id);
            if (token == null)
                throw new ArgumentException("Token non trouvé.");

            token.SessionId = null;
            _context.SaveChangesAsync();
        }

        public async Task<EvaluationToken> GetEvaluationTokenById(string tokenValue)
        {
            EvaluationToken token = await _context.evaluationToken.FirstOrDefaultAsync(e => e.Token == tokenValue);
            if (token == null)
                throw new ArgumentException("Token non trouvé.");
            return token;
        }

        public void CleanExpiredTokens()
        {
            var expiredTokens = _context.evaluationToken
                .Where(t => t.ExpiryDate < DateTime.UtcNow)
            .ToList();

            _context.evaluationToken.RemoveRange(expiredTokens);
            _context.SaveChanges();
        }
    }
}
