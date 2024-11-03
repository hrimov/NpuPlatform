using Microsoft.EntityFrameworkCore;
using NpuBackend.Data.Repositories.Interfaces;
using NpuBackend.Domain.Models;

namespace NpuBackend.Infrastructure.Repositories
{
    public class ElementRepository : IElementRepository
    {
        private readonly NpuDbContext _context;

        public ElementRepository(NpuDbContext context)
        {
            _context = context;
        }

        public async Task<Element?> GetByIdAsync(Guid id) =>
            await _context.Elements.FindAsync(id);

        public async Task<IEnumerable<Element>> GetAllAsync() =>
            await _context.Elements.ToListAsync();

        public async Task AddAsync(Element element)
        {
            await _context.Elements.AddAsync(element);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Element element)
        {
            _context.Elements.Update(element);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var element = await _context.Elements.FindAsync(id);
            if (element != null)
            {
                _context.Elements.Remove(element);
                await _context.SaveChangesAsync();
            }
        }
    }
}
