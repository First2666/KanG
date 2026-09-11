using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KanG.Data;
using KanG.Models;

namespace KanG.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase // คอนโทรลเลอร์จัดการข้อมูลรายการโปรด
    {
        private readonly AppDbContext _context;

        public FavoritesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Favorites/User/5
        [HttpGet("User/{userId}")]
        public async Task<ActionResult<IEnumerable<Favorite>>> GetUserFavorites(int userId)
        {
            return await _context.Favorites.Where(f => f.UserId == userId).ToListAsync();
        }

        // GET: api/Favorites
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Favorite>>> GetFavorites()
        {
            return await _context.Favorites.ToListAsync();
        }

        // GET: api/Favorites/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Favorite>> GetFavorite(int id)
        {
            var item = await _context.Favorites.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }
        
        // POST: api/Favorites
        [HttpPost]
        public async Task<ActionResult<Favorite>> PostFavorite(Favorite item)
        {
            var existing = await _context.Favorites.FirstOrDefaultAsync(f => f.UserId == item.UserId && f.PlaceId == item.PlaceId);
            if (existing != null) return existing; // Already favorited

            _context.Favorites.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFavorite), new { id = item.Id }, item);
        }
        
        // DELETE: api/Favorites/User/1/Place/2
        [HttpDelete("User/{userId}/Place/{placeId}")]
        public async Task<IActionResult> DeleteUserFavorite(int userId, int placeId)
        {
            var item = await _context.Favorites.FirstOrDefaultAsync(f => f.UserId == userId && f.PlaceId == placeId);
            if (item == null)
            {
                return NotFound();
            }

            _context.Favorites.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Favorites/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavorite(int id)
        {
            var item = await _context.Favorites.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Favorites.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
