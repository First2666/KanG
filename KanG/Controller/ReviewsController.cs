using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KanG.Models;
using KanG.Services;
using KanG.Services.IService;

namespace KanG.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase // คอนโทรลเลอร์จัดการข้อมูลรีวิวและคะแนน
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        // GET: api/Reviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
        {
            var reviews = await _reviewService.GetAllReviewsAsync();
            return Ok(reviews);
        }

        // GET: api/Reviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetReview(int id)
        {
            var item = await _reviewService.GetReviewByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }
        
        // POST: api/Reviews
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Review>> PostReview(Review item)
        {
            var created = await _reviewService.CreateReviewAsync(item);
            return CreatedAtAction(nameof(GetReview), new { id = created.Id }, created);
        }
        
        // DELETE: api/Reviews/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var success = await _reviewService.DeleteReviewAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
