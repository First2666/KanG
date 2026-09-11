using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KanG.Data;
using KanG.Models;

namespace KanG.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase // คอนโทรลเลอร์จัดการข้อมูลหมวดหมู่สถานที่
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            if (!await _context.Categories.AnyAsync())
            {
                _context.Categories.AddRange(
                    new Category { Name = "ธรรมชาติ & ภูเขา" },
                    new Category { Name = "ประวัติศาสตร์ & สงครามโลก" },
                    new Category { Name = "น้ำตก & ผืนป่า" },
                    new Category { Name = "คาเฟ่ & ร้านอาหารริมน้ำ" },
                    new Category { Name = "ที่พักแพริมน้ำ & รีสอร์ท" },
                    new Category { Name = "กิจกรรม & แอดเวนเจอร์" }
                );
                await _context.SaveChangesAsync();
            }
            return await _context.Categories.ToListAsync();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }
    }
}
