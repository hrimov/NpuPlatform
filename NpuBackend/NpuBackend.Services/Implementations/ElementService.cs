using NpuBackend.Data.Repositories.Interfaces;
using NpuBackend.Domain.Models;
using NpuBackend.Services.Interfaces;


namespace NpuBackend.Services.Implementations
{
    public class ElementService : IElementService
    {
        private readonly IElementRepository _elementRepository;

        public ElementService(IElementRepository elementRepository)
        {
            _elementRepository = elementRepository;
        }

        public async Task<Element?> GetByIdAsync(Guid id) =>
            await _elementRepository.GetByIdAsync(id);

        public async Task<IEnumerable<Element>> GetAllAsync() =>
            await _elementRepository.GetAllAsync();

        public async Task AddAsync(Element element) =>
            await _elementRepository.AddAsync(element);

        public async Task UpdateAsync(Element element) =>
            await _elementRepository.UpdateAsync(element);

        public async Task DeleteAsync(Guid id) =>
            await _elementRepository.DeleteAsync(id);
    }
}