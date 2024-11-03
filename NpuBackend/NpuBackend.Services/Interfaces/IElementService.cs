using NpuBackend.Domain.Models;

namespace NpuBackend.Services.Interfaces
{
    public interface IElementService
    {
        Task<Element?> GetByIdAsync(Guid id);
        Task<IEnumerable<Element>> GetAllAsync();
        Task AddAsync(Element element);
        Task UpdateAsync(Element element);
        Task DeleteAsync(Guid id);
    }
}
