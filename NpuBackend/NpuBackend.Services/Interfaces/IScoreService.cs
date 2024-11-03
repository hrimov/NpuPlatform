using NpuBackend.Domain.Models;

namespace NpuBackend.Services.Interfaces
{
    public interface IScoreService
    {
        Task<Score?> GetByIdAsync(Guid id);
        Task<IEnumerable<Score>> GetAllScoresForCreationAsync(Guid creationId);
        Task AddAsync(Score score);
        Task UpdateAsync(Score score);
        Task DeleteAsync(Guid id);
    }
}
