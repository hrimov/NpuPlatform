using Microsoft.EntityFrameworkCore;
using NpuBackend.Data.Repositories.Interfaces;
using NpuBackend.Domain.Models;

namespace NpuBackend.Data.Repositories.Implementations;

public class NpuCreationRepository : INpuCreationRepository
{
    private readonly NpuDbContext _context;

    public NpuCreationRepository(NpuDbContext context)
    {
        _context = context;
    }

    public async Task<NpuCreation?> GetByIdAsync(Guid id)
    {
        return await _context.NpuCreations
            .Include(n => n.Elements)
            .Include(n => n.Scores)
            .FirstOrDefaultAsync(n => n.Id == id);
    }

    public async Task<IEnumerable<NpuCreation>> GetAllAsync()
    {
        return await _context.NpuCreations
            .Include(n => n.Elements)
            .Include(n => n.Scores)
            .ToListAsync();
    }

    public async Task AddAsync(NpuCreation creation)
    {
        await _context.NpuCreations.AddAsync(creation);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(NpuCreation creation)
    {
        _context.NpuCreations.Update(creation);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var creation = await GetByIdAsync(id);
        
        if (creation != null)
        {        
            _context.NpuCreations.Remove(creation);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<NpuCreation>> SearchByElementAsync(string elementName) =>
        await _context.NpuCreations
            .Where(c => c.Elements.Any(e => e.Element.Name.Equals(elementName)))
            .ToListAsync();

}
