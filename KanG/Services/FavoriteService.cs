using KanG.Data;
using KanG.Models;
using KanG.Services.IService;
using Microsoft.EntityFrameworkCore;

namespace KanG.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly AppDbContext _context;

        public FavoriteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Favorite>> GetAllFavoritesAsync()
        {
            return await _context.Favorites.ToListAsync();
        }

        public async Task<IEnumerable<Favorite>> GetFavoritesByUserAsync(int userId)
        {
            return await _context.Favorites
                .Where(f => f.UserId == userId)
                .ToListAsync();
        }

        public async Task<Favorite?> GetFavoriteByIdAsync(int id)
        {
            return await _context.Favorites.FindAsync(id);
        }

        public async Task<Favorite> AddFavoriteAsync(Favorite favorite)
        {
            var existing = await _context.Favorites
                .FirstOrDefaultAsync(f => f.UserId == favorite.UserId && f.PlaceId == favorite.PlaceId);

            if (existing != null) return existing;

            _context.Favorites.Add(favorite);
            await _context.SaveChangesAsync();
            return favorite;
        }

        public async Task<bool> DeleteUserFavoriteAsync(int userId, int placeId)
        {
            var item = await _context.Favorites
                .FirstOrDefaultAsync(f => f.UserId == userId && f.PlaceId == placeId);

            if (item == null) return false;

            _context.Favorites.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteFavoriteAsync(int id)
        {
            var item = await _context.Favorites.FindAsync(id);
            if (item == null) return false;

            _context.Favorites.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
