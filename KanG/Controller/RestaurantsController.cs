using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KanG.Data;
using KanG.Models;

namespace KanG.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase // คอนโทรลเลอร์จัดการข้อมูลร้านอาหาร
    {
        private readonly AppDbContext _context;

        public RestaurantsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Restaurants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurants()
        {
            return await _context.Restaurants
                .Include(r => r.Images)
                .Include(r => r.Location)
                .ToListAsync();
        }

        // GET: api/Restaurants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant>> GetRestaurant(int id)
        {
            var item = await _context.Restaurants
                .Include(r => r.Images)
                .Include(r => r.Location)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }
        
        // POST: api/Restaurants
        [HttpPost]
        public async Task<ActionResult<Restaurant>> PostRestaurant(Restaurant item)
        {
            _context.Restaurants.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRestaurant), new { id = item.Id }, item);
        }
        
        // DELETE: api/Restaurants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            var item = await _context.Restaurants.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Restaurants.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
