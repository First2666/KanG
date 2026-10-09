using KanG.Data;
using KanG.Controller;
using KanG.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using KanG.Services.IService;

namespace KanG.Services
{
    public class PlaceService : IPlaceService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PlaceService(AppDbContext context, IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _env = env;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<object>> GetAllPlacesAsync()
        {
            var places = await _context.Places
                .Include(p => p.Images)
                .Include(p => p.Location)
                .Include(p => p.Categories).ThenInclude(c => c.Category)
                .Include(p => p.Tags).ThenInclude(t => t.Tag)
                .OrderByDescending(p => p.Id)
                .ToListAsync();

            return places.Select(p => new
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
                CategoryNames = p.Categories.Where(c => c.Category != null).Select(c => c.Category!.Name).ToList(),
                TagIds = p.Tags.Select(t => t.TagId).ToList(),
                TagNames = p.Tags.Where(t => t.Tag != null).Select(t => t.Tag!.Name).ToList()
            });
        }

        public async Task<object?> GetPlaceByIdAsync(int id)
        {
            var p = await _context.Places
                .Include(p => p.Images)
                .Include(p => p.Location)
                .Include(p => p.Categories).ThenInclude(c => c.Category)
                .Include(p => p.Tags).ThenInclude(t => t.Tag)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (p == null) return null;

            return new
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
                CategoryNames = p.Categories.Where(c => c.Category != null).Select(c => c.Category!.Name).ToList(),
                TagIds = p.Tags.Select(t => t.TagId).ToList(),
                TagNames = p.Tags.Where(t => t.Tag != null).Select(t => t.Tag!.Name).ToList()
            };
        }

        public async Task<object> CreatePlaceAsync(PlaceRequest dto)
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

            return new
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
            };
        }

        public async Task<bool> UpdatePlaceAsync(int id, PlaceRequest dto)
        {
            var place = await _context.Places
                .Include(p => p.Categories)
                .Include(p => p.Tags)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (place == null) return false;

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
            return true;
        }

        public async Task<bool> DeletePlaceAsync(int id)
        {
            var place = await _context.Places.FindAsync(id);
            if (place == null) return false;

            _context.Places.Remove(place);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<PlaceImage?> UploadImageAsync(int id, IFormFile file)
        {
            var place = await _context.Places.FindAsync(id);
            if (place == null || file == null || file.Length == 0) return null;

            var uploadsFolder = Path.Combine(_env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), "images", "places");
            if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var request = _httpContextAccessor.HttpContext?.Request;
            string baseUrl;
            if (request != null)
            {
                baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";
            }
            else
            {
                baseUrl = "http://10.103.0.17/cs67/vue/s17/Kang";
            }

            var imageUrl = $"{baseUrl}/images/places/{fileName}";
            var placeImage = new PlaceImage { PlaceId = id, ImageUrl = imageUrl };
            _context.PlaceImages.Add(placeImage);
            await _context.SaveChangesAsync();

            return placeImage;
        }

        // Attractions
        public async Task<IEnumerable<Attraction>> GetAttractionsAsync()
        {
            return await _context.Attractions
                .Include(a => a.Location)
                .Include(a => a.Images)
                .Include(a => a.Categories).ThenInclude(ac => ac.Category)
                .ToListAsync();
        }

        public async Task<Attraction?> GetAttractionByIdAsync(int id)
        {
            return await _context.Attractions
                .Include(a => a.Location)
                .Include(a => a.Images)
                .Include(a => a.Categories).ThenInclude(ac => ac.Category)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        // Restaurants
        public async Task<IEnumerable<Restaurant>> GetRestaurantsAsync()
        {
            return await _context.Restaurants
                .Include(r => r.Location)
                .Include(r => r.Images)
                .Include(r => r.Categories).ThenInclude(rc => rc.Category)
                .ToListAsync();
        }

        public async Task<Restaurant?> GetRestaurantByIdAsync(int id)
        {
            return await _context.Restaurants
                .Include(r => r.Location)
                .Include(r => r.Images)
                .Include(r => r.Categories).ThenInclude(rc => rc.Category)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        // Accommodations
        public async Task<IEnumerable<Accommodation>> GetAccommodationsAsync()
        {
            return await _context.Accommodations
                .Include(ac => ac.Location)
                .Include(ac => ac.Images)
                .Include(ac => ac.Categories).ThenInclude(acc => acc.Category)
                .ToListAsync();
        }

        public async Task<Accommodation?> GetAccommodationByIdAsync(int id)
        {
            return await _context.Accommodations
                .Include(ac => ac.Location)
                .Include(ac => ac.Images)
                .Include(ac => ac.Categories).ThenInclude(acc => acc.Category)
                .FirstOrDefaultAsync(ac => ac.Id == id);
        }
    }
}
