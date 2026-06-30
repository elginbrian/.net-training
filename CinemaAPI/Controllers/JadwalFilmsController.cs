using CinemaAPI.Data;
using CinemaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JadwalFilmsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public JadwalFilmsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JadwalFilm>>> GetJadwalFilms()
        {
            return await _context.JadwalFilms
                .Include(j => j.Studio)
                .Include(j => j.Film)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JadwalFilm>> GetJadwalFilm(int id)
        {
            var jadwalFilm = await _context.JadwalFilms
                .Include(j => j.Studio)
                .Include(j => j.Film)
                .FirstOrDefaultAsync(j => j.Id == id);

            if (jadwalFilm == null)
            {
                return NotFound();
            }

            return jadwalFilm;
        }

        [HttpPost]
        public async Task<ActionResult<JadwalFilm>> PostJadwalFilm(JadwalFilm jadwalFilm)
        {
            _context.JadwalFilms.Add(jadwalFilm);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetJadwalFilm), new { id = jadwalFilm.Id }, jadwalFilm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutJadwalFilm(int id, JadwalFilm jadwalFilm)
        {
            if (id != jadwalFilm.Id)
            {
                return BadRequest();
            }

            _context.Entry(jadwalFilm).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JadwalFilmExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJadwalFilm(int id)
        {
            var jadwalFilm = await _context.JadwalFilms.FindAsync(id);
            if (jadwalFilm == null)
            {
                return NotFound();
            }

            _context.JadwalFilms.Remove(jadwalFilm);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JadwalFilmExists(int id)
        {
            return _context.JadwalFilms.Any(e => e.Id == id);
        }
    }
}