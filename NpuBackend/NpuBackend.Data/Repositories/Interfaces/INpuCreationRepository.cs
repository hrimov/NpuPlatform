using NpuBackend.Domain.Models;

namespace NpuBackend.Data.Repositories.Interfaces;

public interface INpuCreationRepository
{
    Task<NpuCreation?> GetByIdAsync(Guid id);
    Task<IEnumerable<NpuCreation>> GetAllAsync();
    Task AddAsync(NpuCreation creation);
    Task UpdateAsync(NpuCreation creation);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<NpuCreation>> SearchByElementAsync(string elementName);
}
