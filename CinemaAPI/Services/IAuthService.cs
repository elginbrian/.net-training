using CinemaAPI.Models;

namespace CinemaAPI.Services
{
    public interface IAuthService
    {
        Task<(string Token, string Role)> LoginAsync(string email, string password);
        Task<bool> RegisterAsync(string username, string email, string password);
    }
}
