using KanG.Data;
using KanG.Models;
using KanG.Services.IService;
using Microsoft.EntityFrameworkCore;

namespace KanG.Services
{
    public class LocationService : ILocationService
    {
        private readonly AppDbContext _context;

        public LocationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Location>> GetAllLocationsAsync()
        {
            return await _context.Locations.ToListAsync();
        }

        public async Task<Location?> GetLocationByIdAsync(int id)
        {
            return await _context.Locations.FindAsync(id);
        }

        public async Task<Location> CreateLocationAsync(Location location)
        {
            _context.Locations.Add(location);
            await _context.SaveChangesAsync();
            return location;
        }

        public async Task<bool> DeleteLocationAsync(int id)
        {
            var item = await _context.Locations.FindAsync(id);
            if (item == null) return false;

            _context.Locations.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
