using CinemaAPI.Data;
using CinemaAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CinemaAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        public AuthService(IConfiguration configuration, AppDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<(string Token, string Role)> LoginAsync(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) throw new UnauthorizedAccessException("Email tidak ditemukan.");

            bool isPasswordValid = false;
            if (user.PasswordHash.StartsWith("$2a$") || user.PasswordHash.StartsWith("$2b$") || user.PasswordHash.StartsWith("$2y$"))
            {
                isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            }
            else
            {
                isPasswordValid = (password == user.PasswordHash);
            }

            if (!isPasswordValid) throw new UnauthorizedAccessException("Password salah.");

            var token = GenerateJwtToken(user.Email, user.Role);
            return (token, user.Role);
        }

        public async Task<bool> RegisterAsync(string username, string email, string password)
        {
            if (await _context.Users.AnyAsync(u => u.Email == email)) return false;

            var newUser = new User
            {
                Username = username,
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                Role = "User"
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return true;
        }

        private string GenerateJwtToken(string email, string role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? string.Empty));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
