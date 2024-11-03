using NpuBackend.Domain.Models;

namespace NpuBackend.Services.Interfaces
{
    public interface INpuCreationService
    {
        Task<NpuCreation?> GetByIdAsync(Guid id);
        Task<IEnumerable<NpuCreation>> GetAllAsync();
        Task<IEnumerable<NpuCreation>> SearchByElementAsync(string elementName);
        Task<NpuCreation> CreateAsync(NpuCreation creation);
        Task UpdateAsync(NpuCreation creation);
        Task DeleteAsync(Guid id);
        Task AddScoreAsync(Guid creationId, Guid userId, int score);
    }
}
