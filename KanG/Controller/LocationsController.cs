using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KanG.Data;
using KanG.Models;

namespace KanG.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase // คอนโทรลเลอร์จัดการข้อมูลพิกัดตำแหน่ง
    {
        private readonly AppDbContext _context;

        public LocationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Locations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Location>>> GetLocations()
        {
            return await _context.Locations.ToListAsync();
        }

        // GET: api/Locations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Location>> GetLocation(int id)
        {
            var item = await _context.Locations.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }
        
        // POST: api/Locations
        [HttpPost]
        public async Task<ActionResult<Location>> PostLocation(Location item)
        {
            _context.Locations.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLocation), new { id = item.Id }, item);
        }
        
        // DELETE: api/Locations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            var item = await _context.Locations.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Locations.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
