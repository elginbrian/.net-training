using CinemaAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CinemaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>User Login</summary>
        /// <remarks>Login Pengguna (Mendapatkan Token JWT).</remarks>
        /// <param name="request">Gunakan email "admin@cinema.com" dan password "admin123" untuk login sebagai Admin.</param>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authService.LoginAsync(request.Email, request.Password);
            return Ok(new { Token = result.Token, Role = result.Role });
        }

        /// <summary>User Register</summary>
        /// <remarks>Mendaftarkan Pengguna Baru.</remarks>
        /// <param name="request">Data registrasi pengguna baru (Role default adalah User).</param>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] LoginRequest request)
        {
            var isSuccess = await _authService.RegisterAsync(request.Username, request.Email, request.Password);
            if (!isSuccess) return BadRequest("Email sudah terpakai.");
            
            return Ok("Registrasi berhasil!");
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
