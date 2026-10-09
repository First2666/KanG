using KanG.Controller;
using KanG.Models;
using Microsoft.AspNetCore.Http;

namespace KanG.Services.IService
{
    public interface IPlaceService
    {
        Task<IEnumerable<object>> GetAllPlacesAsync();
        Task<object?> GetPlaceByIdAsync(int id);
        Task<object> CreatePlaceAsync(PlaceRequest request);
        Task<bool> UpdatePlaceAsync(int id, PlaceRequest request);
        Task<bool> DeletePlaceAsync(int id);
        Task<PlaceImage?> UploadImageAsync(int id, IFormFile file);

        Task<IEnumerable<Attraction>> GetAttractionsAsync();
        Task<Attraction?> GetAttractionByIdAsync(int id);

        Task<IEnumerable<Restaurant>> GetRestaurantsAsync();
        Task<Restaurant?> GetRestaurantByIdAsync(int id);

        Task<IEnumerable<Accommodation>> GetAccommodationsAsync();
        Task<Accommodation?> GetAccommodationByIdAsync(int id);
    }
}
