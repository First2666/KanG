using KanG.Data;
using KanG.Models;
using KanG.Services.IService;
using Microsoft.EntityFrameworkCore;

namespace KanG.Services
{
    public class PlaceImageService : IPlaceImageService
    {
        private readonly AppDbContext _context;

        public PlaceImageService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PlaceImage>> GetAllPlaceImagesAsync()
        {
            return await _context.PlaceImages.ToListAsync();
        }

        public async Task<PlaceImage?> GetPlaceImageByIdAsync(int id)
        {
            return await _context.PlaceImages.FindAsync(id);
        }

        public async Task<PlaceImage> CreatePlaceImageAsync(PlaceImage item)
        {
            _context.PlaceImages.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> DeletePlaceImageAsync(int id)
        {
            var item = await _context.PlaceImages.FindAsync(id);
            if (item == null) return false;

            try
            {
                if (!string.IsNullOrEmpty(item.ImageUrl))
                {
                    var marker = "/images/places/";
                    var index = item.ImageUrl.IndexOf(marker, StringComparison.OrdinalIgnoreCase);
                    if (index >= 0)
                    {
                        var fileName = item.ImageUrl.Substring(index + marker.Length).TrimStart('/');
                        var qIdx = fileName.IndexOf('?');
                        if (qIdx >= 0) fileName = fileName.Substring(0, qIdx);

                        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "places");
                        var fullPath = Path.Combine(uploadsFolder, fileName);
                        if (File.Exists(fullPath))
                        {
                            File.Delete(fullPath);
                        }
                    }
                }
            }
            catch
            {
                // Ignore file deletion exceptions
            }

            _context.PlaceImages.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
