using NpuBackend.Domain.Models;

namespace NpuBackend.Data.Repositories.Interfaces
{
    public interface IElementRepository
    {
        Task<Element?> GetByIdAsync(Guid id);
        Task<IEnumerable<Element>> GetAllAsync();
        Task AddAsync(Element element);
        Task UpdateAsync(Element element);
        Task DeleteAsync(Guid id);
    }
}
