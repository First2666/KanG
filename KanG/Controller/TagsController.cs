using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KanG.Data;
using KanG.Models;

namespace KanG.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase // คอนโทรลเลอร์จัดการข้อมูลป้ายกำกับ
    {
        private readonly AppDbContext _context;

        public TagsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Tags
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tag>>> GetTags()
        {
            if (!await _context.Tags.AnyAsync())
            {
                _context.Tags.AddRange(
                    new Tag { Name = "วิวสวยถ่ายรูปปัง" },
                    new Tag { Name = "ที่จอดรถสะดวก" },
                    new Tag { Name = "บรรยากาศดีริมน้ำ" },
                    new Tag { Name = "เหมาะสำหรับครอบครัว" },
                    new Tag { Name = "มีมุมกาแฟ" },
                    new Tag { Name = "สัตว์เลี้ยงเข้าได้" },
                    new Tag { Name = "เปิดให้บริการทุกวัน" },
                    new Tag { Name = "มีWi-Fiฟรี" }
                );
                await _context.SaveChangesAsync();
            }
            return await _context.Tags.ToListAsync();
        }

        // GET: api/Tags/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tag>> GetTag(int id)
        {
            var item = await _context.Tags.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }
        
        // POST: api/Tags
        [HttpPost]
        public async Task<ActionResult<Tag>> PostTag(Tag item)
        {
            _context.Tags.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTag), new { id = item.Id }, item);
        }
        
        // DELETE: api/Tags/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            var item = await _context.Tags.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Tags.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
