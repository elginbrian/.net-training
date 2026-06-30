using CinemaAPI.Data;
using Microsoft.AspNetCore.Authorization;
using CinemaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiketsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TiketsController(AppDbContext context)
        {
            _context = context;
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
            _context.Tikets.Add(tiket);
            await _context.SaveChangesAsync();

            var createdTiket = await _context.Tikets
                .Include(t => t.JadwalFilm)
                .ThenInclude(j => j!.Film)
                .Include(t => t.JadwalFilm)
                .ThenInclude(j => j!.Studio)
                .FirstOrDefaultAsync(t => t.Id == tiket.Id);

            return CreatedAtAction(nameof(GetTiket), new { id = tiket.Id }, createdTiket);
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