using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KanG.Data;
using KanG.Models;

namespace KanG.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase // คอนโทรลเลอร์จัดการข้อมูลรีวิวและคะแนน
    {
        private readonly AppDbContext _context;

        public ReviewsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Reviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
        {
            return await _context.Reviews.ToListAsync();
        }

        // GET: api/Reviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetReview(int id)
        {
            var item = await _context.Reviews.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }
        
        // POST: api/Reviews
        [HttpPost]
        public async Task<ActionResult<Review>> PostReview(Review item)
        {
            _context.Reviews.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReview), new { id = item.Id }, item);
        }
        
        // DELETE: api/Reviews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var item = await _context.Reviews.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Reviews.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
