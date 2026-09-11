using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KanG.Data;
using KanG.Models;

namespace KanG.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttractionsController : ControllerBase // คอนโทรลเลอร์จัดการข้อมูลสถานที่ท่องเที่ยว
    {
        private readonly AppDbContext _context;

        public AttractionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Attractions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attraction>>> GetAttractions()
        {
            return await _context.Attractions
                .Include(a => a.Location)
                .Include(a => a.Images)
                .Include(a => a.Categories)
                    .ThenInclude(ac => ac.Category)
                .ToListAsync();
        }

        // GET: api/Attractions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Attraction>> GetAttraction(int id)
        {
            var attraction = await _context.Attractions
                .Include(a => a.Location)
                .Include(a => a.Images)
                .Include(a => a.Categories)
                    .ThenInclude(ac => ac.Category)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (attraction == null)
            {
                return NotFound();
            }

            return attraction;
        }
    }
}
