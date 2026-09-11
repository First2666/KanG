using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KanG.Data;
using KanG.Models;

namespace KanG.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceImagesController : ControllerBase // คอนโทรลเลอร์จัดการข้อมูลรูปภาพสถานที่
    {
        private readonly AppDbContext _context;

        public PlaceImagesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/PlaceImages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlaceImage>>> GetPlaceImages()
        {
            return await _context.PlaceImages.ToListAsync();
        }

        // GET: api/PlaceImages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlaceImage>> GetPlaceImage(int id)
        {
            var item = await _context.PlaceImages.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }
        
        // POST: api/PlaceImages
        [HttpPost]
        public async Task<ActionResult<PlaceImage>> PostPlaceImage(PlaceImage item)
        {
            _context.PlaceImages.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPlaceImage), new { id = item.Id }, item);
        }
        
        // DELETE: api/PlaceImages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlaceImage(int id)
        {
            var item = await _context.PlaceImages.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.PlaceImages.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
