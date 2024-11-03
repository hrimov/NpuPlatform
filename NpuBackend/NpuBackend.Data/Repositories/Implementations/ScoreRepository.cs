using Microsoft.EntityFrameworkCore;
using NpuBackend.Data.Repositories.Interfaces;
using NpuBackend.Domain.Models;

namespace NpuBackend.Data.Repositories.Implementations
{
    public class ScoreRepository : IScoreRepository
    {
        private readonly NpuDbContext _context;

        public ScoreRepository(NpuDbContext context)
        {
            _context = context;
        }

        public async Task<Score?> GetByIdAsync(Guid id) =>
            await _context.Scores.FindAsync(id);

        public async Task<IEnumerable<Score>> GetAllScoresForCreationAsync(Guid creationId) =>
            await _context.Scores
                .Where(score => score.NpuCreationId == creationId)
                .ToListAsync();

        public async Task AddAsync(Score score)
        {
            await _context.Scores.AddAsync(score);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Score score)
        {
            _context.Scores.Update(score);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var score = await _context.Scores.FindAsync(id);
            if (score != null)
            {
                _context.Scores.Remove(score);
                await _context.SaveChangesAsync();
            }
        }
    }
}
