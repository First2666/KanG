using KanG.Models;

namespace KanG.Services.IService
{
    public interface ILocationService
    {
        Task<IEnumerable<Location>> GetAllLocationsAsync();
        Task<Location?> GetLocationByIdAsync(int id);
        Task<Location> CreateLocationAsync(Location location);
        Task<bool> DeleteLocationAsync(int id);
    }
}
