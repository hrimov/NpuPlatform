using NpuBackend.Data.Repositories.Interfaces;
using NpuBackend.Domain.Models;
using NpuBackend.Services.Interfaces;

namespace NpuBackend.Services.Implementations
{
    public class ScoreService : IScoreService
    {
        private readonly IScoreRepository _scoreRepository;

        public ScoreService(IScoreRepository scoreRepository)
        {
            _scoreRepository = scoreRepository;
        }

        public async Task<Score?> GetByIdAsync(Guid id) =>
            await _scoreRepository.GetByIdAsync(id);

        public async Task<IEnumerable<Score>> GetAllScoresForCreationAsync(Guid creationId) =>
            await _scoreRepository.GetAllScoresForCreationAsync(creationId);

        public async Task AddAsync(Score score) =>
            await _scoreRepository.AddAsync(score);

        public async Task UpdateAsync(Score score) =>
            await _scoreRepository.UpdateAsync(score);

        public async Task DeleteAsync(Guid id) =>
            await _scoreRepository.DeleteAsync(id);
    }
}