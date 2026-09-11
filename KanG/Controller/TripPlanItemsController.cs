using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KanG.Data;
using KanG.Models;

namespace KanG.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripPlanItemsController : ControllerBase // คอนโทรลเลอร์จัดการข้อมูลรายการในแผนทริป
    {
        private readonly AppDbContext _context;

        public TripPlanItemsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/TripPlanItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TripPlanItem>>> GetTripPlanItems()
        {
            return await _context.TripPlanItems.ToListAsync();
        }

        // GET: api/TripPlanItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TripPlanItem>> GetTripPlanItem(int id)
        {
            var item = await _context.TripPlanItems.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }
        
        // POST: api/TripPlanItems
        [HttpPost]
        public async Task<ActionResult<TripPlanItem>> PostTripPlanItem(TripPlanItem item)
        {
            _context.TripPlanItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTripPlanItem), new { id = item.Id }, item);
        }
        
        // PUT: api/TripPlanItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTripPlanItem(int id, TripPlanItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TripPlanItemExists(id))
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

        // DELETE: api/TripPlanItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTripPlanItem(int id)
        {
            var item = await _context.TripPlanItems.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.TripPlanItems.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TripPlanItemExists(int id)
        {
            return _context.TripPlanItems.Any(e => e.Id == id);
        }
    }
}
