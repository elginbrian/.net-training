using CinemaAPI.Data;
using CinemaAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudiosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudiosController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>Get All Studios</summary>
        /// <remarks>Mendapatkan daftar seluruh Studio (Bisa diakses umum).</remarks>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Studio>>> GetStudios()
        {
            return await _context.Studios.ToListAsync();
        }

        /// <summary>Get Studio by ID</summary>
        /// <remarks>Mendapatkan detail Studio berdasarkan ID (Bisa diakses umum).</remarks>
        [HttpGet("{id}")]
        public async Task<ActionResult<Studio>> GetStudio(int id)
        {
            var studio = await _context.Studios.FindAsync(id);

            if (studio == null)
            {
                return NotFound();
            }

            return studio;
        }

        /// <summary>Update Studio</summary>
        /// <remarks>Mengubah data Studio yang sudah ada (Khusus Admin).</remarks>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudio(int id, Studio studio)
        {
            if (id != studio.Id)
            {
                return BadRequest();
            }

            _context.Entry(studio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudioExists(id))
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

        /// <summary>Create Studio</summary>
        /// <remarks>Menambahkan Studio baru (Khusus Admin).</remarks>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Studio>> PostStudio(Studio studio)
        {
            _context.Studios.Add(studio);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudio), new { id = studio.Id }, studio);
        }

        /// <summary>Delete Studio</summary>
        /// <remarks>Menghapus Studio berdasarkan ID (Khusus Admin).</remarks>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudio(int id)
        {
            var studio = await _context.Studios.FindAsync(id);
            if (studio == null)
            {
                return NotFound();
            }

            _context.Studios.Remove(studio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudioExists(int id)
        {
            return _context.Studios.Any(e => e.Id == id);
        }
    }
}
