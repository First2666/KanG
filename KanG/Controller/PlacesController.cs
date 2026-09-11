using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KanG.Data;
using KanG.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace KanG.Controller
{
    public class PlaceDto
    {
        public string PlaceType { get; set; } = "Attraction";
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int LocationId { get; set; } = 1;
        public decimal EntranceFee { get; set; } = 0;
        public TimeSpan? OpeningTime { get; set; }
        public TimeSpan? ClosingTime { get; set; }

        public List<int> CategoryIds { get; set; } = new List<int>();
        public List<int> TagIds { get; set; } = new List<int>();
    }

    [Route("api/[controller]")]
    [ApiController]
    public class PlacesController : ControllerBase // คอนโทรลเลอร์จัดการข้อมูลสถานที่ (หลัก)
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public PlacesController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetPlaces()
        {
            var places = await _context.Places
                .Include(p => p.Images)
                .Include(p => p.Location)
                .Include(p => p.Categories).ThenInclude(c => c.Category)
                .Include(p => p.Tags).ThenInclude(t => t.Tag)
                .OrderByDescending(p => p.Id)
                .ToListAsync();

            return Ok(places.Select(p => new
            {
                p.Id,
                p.Name,
                p.Description,
                p.Latitude,
                p.Longitude,
                p.LocationId,
                Location = p.Location,
                PlaceType = p.GetType().Name,
                EntranceFee = p is Attraction a ? a.EntranceFee : (p is Accommodation ac ? ac.PricePerNight : 0),
                OpeningTime = p.OpeningTime,
                ClosingTime = p.ClosingTime,
                Images = p.Images,
                CategoryIds = p.Categories.Select(c => c.CategoryId).ToList(),
                CategoryNames = p.Categories.Where(c => c.Category != null).Select(c => c.Category.Name).ToList(),
                TagIds = p.Tags.Select(t => t.TagId).ToList(),
                TagNames = p.Tags.Where(t => t.Tag != null).Select(t => t.Tag.Name).ToList()
            }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetPlace(int id)
        {
            var p = await _context.Places
                .Include(p => p.Images)
                .Include(p => p.Location)
                .Include(p => p.Categories).ThenInclude(c => c.Category)
                .Include(p => p.Tags).ThenInclude(t => t.Tag)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (p == null) return NotFound();

            return Ok(new
            {
                p.Id,
                p.Name,
                p.Description,
                p.Latitude,
                p.Longitude,
                p.LocationId,
                Location = p.Location,
                PlaceType = p.GetType().Name,
                EntranceFee = p is Attraction a ? a.EntranceFee : (p is Accommodation ac ? ac.PricePerNight : 0),
                OpeningTime = p.OpeningTime,
                ClosingTime = p.ClosingTime,
                Images = p.Images,
                CategoryIds = p.Categories.Select(c => c.CategoryId).ToList(),
                CategoryNames = p.Categories.Where(c => c.Category != null).Select(c => c.Category.Name).ToList(),
                TagIds = p.Tags.Select(t => t.TagId).ToList(),
                TagNames = p.Tags.Where(t => t.Tag != null).Select(t => t.Tag.Name).ToList()
            });
        }

        [HttpPost]
        public async Task<ActionResult<object>> PostPlace([FromBody] PlaceDto dto)
        {
            Place place;
            if (dto.PlaceType == "Restaurant")
            {
                place = new Restaurant { FoodType = KanG.Enums.FoodType.Local };
            }
            else if (dto.PlaceType == "Accommodation")
            {
                place = new Accommodation { PricePerNight = dto.EntranceFee, MaxGuests = 2 };
            }
            else
            {
                place = new Attraction { EntranceFee = dto.EntranceFee };
            }

            place.Name = dto.Name;
            place.Description = dto.Description;
            place.Latitude = dto.Latitude;
            place.Longitude = dto.Longitude;
            place.LocationId = dto.LocationId;
            place.OpeningTime = dto.OpeningTime;
            place.ClosingTime = dto.ClosingTime;
            place.CreatedAt = DateTime.UtcNow;
            place.UpdatedAt = DateTime.UtcNow;

            foreach (var cid in dto.CategoryIds)
                place.Categories.Add(new PlaceCategory { CategoryId = cid });

            foreach (var tid in dto.TagIds)
                place.Tags.Add(new PlaceTag { TagId = tid });

            _context.Places.Add(place);
            await _context.SaveChangesAsync();

            // Return full object to match GET schema for frontend
            return Ok(new
            {
                place.Id,
                place.Name,
                place.Description,
                place.Latitude,
                place.Longitude,
                place.LocationId,
                PlaceType = place.GetType().Name,
                EntranceFee = dto.EntranceFee,
                OpeningTime = place.OpeningTime,
                ClosingTime = place.ClosingTime,
                Images = new List<PlaceImage>(),
                CategoryIds = dto.CategoryIds,
                TagIds = dto.TagIds
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlace(int id, [FromBody] PlaceDto dto)
        {
            var place = await _context.Places
                .Include(p => p.Categories)
                .Include(p => p.Tags)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (place == null) return NotFound();

            place.Name = dto.Name;
            place.Description = dto.Description;
            place.Latitude = dto.Latitude;
            place.Longitude = dto.Longitude;
            place.LocationId = dto.LocationId;
            place.OpeningTime = dto.OpeningTime;
            place.ClosingTime = dto.ClosingTime;
            place.UpdatedAt = DateTime.UtcNow;

            if (place is Attraction a) a.EntranceFee = dto.EntranceFee;
            if (place is Accommodation ac) ac.PricePerNight = dto.EntranceFee;

            // Update Categories
            _context.PlaceCategories.RemoveRange(place.Categories);
            foreach (var cid in dto.CategoryIds)
                place.Categories.Add(new PlaceCategory { CategoryId = cid, PlaceId = place.Id });

            // Update Tags
            _context.PlaceTags.RemoveRange(place.Tags);
            foreach (var tid in dto.TagIds)
                place.Tags.Add(new PlaceTag { TagId = tid, PlaceId = place.Id });

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlace(int id)
        {
            var place = await _context.Places.FindAsync(id);
            if (place == null) return NotFound();

            _context.Places.Remove(place);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Upload Images Endpoint
        [HttpPost("{id}/Images")]
        public async Task<IActionResult> UploadImage(int id, IFormFile file)
        {
            var place = await _context.Places.FindAsync(id);
            if (place == null) return NotFound(new { message = "Place not found" });

            if (file == null || file.Length == 0) return BadRequest(new { message = "No file uploaded" });

            var uploadsFolder = Path.Combine(_env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), "images", "places");
            if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var imageUrl = $"http://localhost:5088/images/places/{fileName}";
            var placeImage = new PlaceImage { PlaceId = id, ImageUrl = imageUrl };
            _context.PlaceImages.Add(placeImage);
            await _context.SaveChangesAsync();

            return Ok(placeImage);
        }
    }
}
