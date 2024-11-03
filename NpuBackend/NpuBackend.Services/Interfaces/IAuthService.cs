using NpuBackend.Domain.Models;

namespace NpuBackend.Services.Interfaces 
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(string username, string email, string password);
        Task<string?> LoginAsync(string username, string password);
        Task<User?> GetUserByIdAsync(Guid userId);
    }
}
