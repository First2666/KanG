using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KanG.Data;
using KanG.Models;

namespace KanG.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripPlansController : ControllerBase // คอนโทรลเลอร์จัดการข้อมูลแผนการเดินทาง
    {
        private readonly AppDbContext _context;

        public TripPlansController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/TripPlans/User/1
        [HttpGet("User/{userId}")]
        public async Task<ActionResult<IEnumerable<TripPlan>>> GetTripPlansByUser(int userId)
        {
            return await _context.TripPlans
                .Include(tp => tp.Items)
                .Where(tp => tp.UserId == userId)
                .OrderByDescending(tp => tp.StartDate)
                .ToListAsync();
        }

        // GET: api/TripPlans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TripPlan>> GetTripPlan(int id)
        {
            var tripPlan = await _context.TripPlans
                .Include(tp => tp.Items)
                    .ThenInclude(i => i.Place)
                        .ThenInclude(p => p.Location)
                .Include(tp => tp.Items)
                    .ThenInclude(i => i.Place)
                        .ThenInclude(p => p.Images)
                .FirstOrDefaultAsync(tp => tp.Id == id);

            if (tripPlan == null)
            {
                return NotFound();
            }

            // Client-side can sort the items by DayNumber and Sequence
            return tripPlan;
        }
        // POST: api/TripPlans
        [HttpPost]
        public async Task<ActionResult<TripPlan>> PostTripPlan(TripPlan tripPlan)
        {
            _context.TripPlans.Add(tripPlan);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTripPlan", new { id = tripPlan.Id }, tripPlan);
        }

        // PUT: api/TripPlans/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTripPlan(int id, TripPlan tripPlan)
        {
            if (id != tripPlan.Id)
            {
                return BadRequest();
            }

            _context.Entry(tripPlan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TripPlanExists(id))
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

        // DELETE: api/TripPlans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTripPlan(int id)
        {
            var tripPlan = await _context.TripPlans.FindAsync(id);
            if (tripPlan == null)
            {
                return NotFound();
            }

            _context.TripPlans.Remove(tripPlan);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TripPlanExists(int id)
        {
            return _context.TripPlans.Any(e => e.Id == id);
        }
    }
}
