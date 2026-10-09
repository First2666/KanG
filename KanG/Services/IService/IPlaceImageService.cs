using KanG.Models;

namespace KanG.Services.IService
{
    public interface IPlaceImageService
    {
        Task<IEnumerable<PlaceImage>> GetAllPlaceImagesAsync();
        Task<PlaceImage?> GetPlaceImageByIdAsync(int id);
        Task<PlaceImage> CreatePlaceImageAsync(PlaceImage item);
        Task<bool> DeletePlaceImageAsync(int id);
    }
}
