using KanG.Models;

namespace KanG.Services.IService
{
    public interface IFavoriteService
    {
        Task<IEnumerable<Favorite>> GetAllFavoritesAsync();
        Task<IEnumerable<Favorite>> GetFavoritesByUserAsync(int userId);
        Task<Favorite?> GetFavoriteByIdAsync(int id);
        Task<Favorite> AddFavoriteAsync(Favorite favorite);
        Task<bool> DeleteUserFavoriteAsync(int userId, int placeId);
        Task<bool> DeleteFavoriteAsync(int id);
    }
}
