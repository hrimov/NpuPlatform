using NpuBackend.Domain.Models;

using NpuBackend.Data.Repositories.Interfaces;
using NpuBackend.Services.Interfaces;

namespace NpuBackend.Services.Implementations
{
    public class NpuCreationService : INpuCreationService
    {
        private readonly INpuCreationRepository _npuCreationRepository;
        private readonly IElementRepository _elementRepository;
        private readonly IScoreRepository _scoreRepository;

        public NpuCreationService(INpuCreationRepository npuCreationRepository, IElementRepository elementRepository, IScoreRepository scoreRepository)
        {
            _npuCreationRepository = npuCreationRepository;
            _elementRepository = elementRepository;
            _scoreRepository = scoreRepository;
        }

        public async Task<NpuCreation?> GetByIdAsync(Guid id) =>
            await _npuCreationRepository.GetByIdAsync(id);

        public async Task<IEnumerable<NpuCreation>> GetAllAsync() =>
            await _npuCreationRepository.GetAllAsync();

        public async Task<IEnumerable<NpuCreation>> SearchByElementAsync(string elementName) =>
            await _npuCreationRepository.SearchByElementAsync(elementName);

        public async Task<NpuCreation> CreateAsync(NpuCreation creation)
        {
            await _npuCreationRepository.AddAsync(creation);
            return creation;
        }

        public async Task UpdateAsync(NpuCreation creation) =>
            await _npuCreationRepository.UpdateAsync(creation);

        public async Task DeleteAsync(Guid id) =>
            await _npuCreationRepository.DeleteAsync(id);

        public async Task AddScoreAsync(Guid creationId, Guid userId, int score)
        {
            var scoreEntity = new Score
            {
                UserId = userId,
                NpuCreationId = creationId,
                Value = score,
            };
            await _scoreRepository.AddAsync(scoreEntity);
        }
    }
}
