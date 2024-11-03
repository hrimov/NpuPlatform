using Microsoft.EntityFrameworkCore;
using NpuBackend.Data.Repositories.Interfaces;
using NpuBackend.Domain.Models;

namespace NpuBackend.Data.Repositories.Implementations;

public class UserRepository : IUserRepository
{
    private readonly NpuDbContext _context;

    public UserRepository(NpuDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(Guid id) => 
        await _context.Users.FindAsync(id);
    
    public async Task<User?> GetByEmailAsync(string email) => 
        await _context.Users.Where(e => e.Email == email).FirstOrDefaultAsync();
    
    public async Task<User?> GetByUsernameAsync(string username) =>
        await _context.Users.Where(e => e.Username == username).FirstOrDefaultAsync();

    public async Task<IEnumerable<User>> GetAllAsync() => 
        await _context.Users.ToListAsync();

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await GetByIdAsync(id);

        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

    }
}
