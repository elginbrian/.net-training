using CinemaAPI.Data;
using Microsoft.AspNetCore.Authorization;
using CinemaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using CinemaAPI.Services;

namespace CinemaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiketsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ITiketService _tiketService;

        public TiketsController(AppDbContext context, ITiketService tiketService)
        {
            _context = context;
            _tiketService = tiketService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tiket>>> GetTikets()
        {
            return await _context.Tikets
                .Include(t => t.JadwalFilm)
                .ThenInclude(j => j!.Film)
                .Include(t => t.JadwalFilm)
                .ThenInclude(j => j!.Studio)
                .ToListAsync();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Tiket>> GetTiket(int id)
        {
            var tiket = await _context.Tikets
                .Include(t => t.JadwalFilm)
                .ThenInclude(j => j!.Film)
                .Include(t => t.JadwalFilm)
                .ThenInclude(j => j!.Studio)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tiket == null)
            {
                return NotFound();
            }

            return tiket;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Tiket>> PostTiket(Tiket tiket)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email) ?? string.Empty;

            try
            {
                var createdTiket = await _tiketService.PesanTiketAsync(tiket, userEmail);
                return CreatedAtAction(nameof(GetTiket), new { id = createdTiket.Id }, createdTiket);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTiket(int id, Tiket tiket)
        {
            if (id != tiket.Id)
            {
                return BadRequest();
            }

            _context.Entry(tiket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TiketExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTiket(int id)
        {
            var tiket = await _context.Tikets.FindAsync(id);
            if (tiket == null)
            {
                return NotFound();
            }

            _context.Tikets.Remove(tiket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TiketExists(int id)
        {
            return _context.Tikets.Any(e => e.Id == id);
        }
    }
}