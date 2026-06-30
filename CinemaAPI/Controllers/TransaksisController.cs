using CinemaAPI.Data;
using CinemaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransaksisController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TransaksisController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>Get All Transaksi</summary>
        /// <remarks>Mendapatkan seluruh daftar Transaksi pembayaran.</remarks>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaksi>>> GetTransaksis()
        {
            return await _context.Transaksis
                .Include(t => t.Tiket)
                .ToListAsync();
        }

        /// <summary>Get Transaksi by ID</summary>
        /// <remarks>Mendapatkan detail Transaksi berdasarkan ID.</remarks>
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaksi>> GetTransaksi(int id)
        {
            var transaksi = await _context.Transaksis
                .Include(t => t.Tiket)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (transaksi == null)
            {
                return NotFound();
            }

            return transaksi;
        }

        /// <summary>Create Transaksi</summary>
        /// <remarks>Membuat Transaksi / Pembayaran baru untuk Tiket yang dipesan.</remarks>
        [HttpPost]
        public async Task<ActionResult<Transaksi>> PostTransaksi(Transaksi transaksi)
        {
            _context.Transaksis.Add(transaksi);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTransaksi), new { id = transaksi.Id }, transaksi);
        }

        /// <summary>Update Transaksi</summary>
        /// <remarks>Mengubah status Transaksi (Khusus Admin).</remarks>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaksi(int id, Transaksi transaksi)
        {
            if (id != transaksi.Id)
            {
                return BadRequest();
            }

            _context.Entry(transaksi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransaksiExists(id))
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

        /// <summary>Delete Transaksi</summary>
        /// <remarks>Menghapus Transaksi (Khusus Admin).</remarks>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaksi(int id)
        {
            var transaksi = await _context.Transaksis.FindAsync(id);
            if (transaksi == null)
            {
                return NotFound();
            }

            _context.Transaksis.Remove(transaksi);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TransaksiExists(int id)
        {
            return _context.Transaksis.Any(e => e.Id == id);
        }
    }
}
