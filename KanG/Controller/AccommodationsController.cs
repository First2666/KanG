using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KanG.Data;
using KanG.Models;

namespace KanG.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccommodationsController : ControllerBase // คอนโทรลเลอร์จัดการข้อมูลที่พัก
    {
        private readonly AppDbContext _context;

        public AccommodationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Accommodations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Accommodation>>> GetAccommodations()
        {
            return await _context.Accommodations
                .Include(a => a.Images)
                .Include(a => a.Location)
                .ToListAsync();
        }

        // GET: api/Accommodations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Accommodation>> GetAccommodation(int id)
        {
            var item = await _context.Accommodations
                .Include(a => a.Images)
                .Include(a => a.Location)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }
        
        // POST: api/Accommodations
        [HttpPost]
        public async Task<ActionResult<Accommodation>> PostAccommodation(Accommodation item)
        {
            _context.Accommodations.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAccommodation), new { id = item.Id }, item);
        }
        
        // DELETE: api/Accommodations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccommodation(int id)
        {
            var item = await _context.Accommodations.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Accommodations.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
