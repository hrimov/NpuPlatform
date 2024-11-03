using NpuBackend.Domain.Models;

namespace NpuBackend.Data.Repositories.Interfaces
{
    public interface IScoreRepository
    {
        Task<Score?> GetByIdAsync(Guid id);
        Task<IEnumerable<Score>> GetAllScoresForCreationAsync(Guid creationId);
        Task AddAsync(Score score);
        Task UpdateAsync(Score score);
        Task DeleteAsync(Guid id);
    }
}
