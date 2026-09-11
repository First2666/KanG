using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KanG.Data;
using KanG.Models;

namespace KanG.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase // คอนโทรลเลอร์จัดการข้อมูลกิจกรรมและเทศกาล
    {
        private readonly AppDbContext _context;

        public EventsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Events
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            return await _context.Events.ToListAsync();
        }

        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            var item = await _context.Events.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }
        
        // POST: api/Events
        [HttpPost]
        public async Task<ActionResult<Event>> PostEvent(Event item)
        {
            _context.Events.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEvent), new { id = item.Id }, item);
        }
        
        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var item = await _context.Events.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Events.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
