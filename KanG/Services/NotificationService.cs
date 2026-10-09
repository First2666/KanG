using KanG.Data;
using KanG.Models;
using KanG.Services.IService;
using Microsoft.EntityFrameworkCore;

namespace KanG.Services
{
    public class NotificationService : INotificationService
    {
        private readonly AppDbContext _context;

        public NotificationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Notification>> GetUserNotificationsAsync(int userId, bool unreadOnly = false)
        {
            var query = _context.Notifications
                .Where(n => n.UserId == userId);

            if (unreadOnly)
            {
                query = query.Where(n => !n.IsRead);
            }

            return await query
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }

        public async Task<int> GetUnreadCountAsync(int userId)
        {
            return await _context.Notifications
                .CountAsync(n => n.UserId == userId && !n.IsRead);
        }

        public async Task<Notification?> GetNotificationByIdAsync(int id, int userId)
        {
            return await _context.Notifications
                .FirstOrDefaultAsync(n => n.Id == id && n.UserId == userId);
        }

        public async Task<bool> MarkAsReadAsync(int id, int userId)
        {
            var item = await _context.Notifications
                .FirstOrDefaultAsync(n => n.Id == id && n.UserId == userId);

            if (item == null) return false;

            if (!item.IsRead)
            {
                item.IsRead = true;
                await _context.SaveChangesAsync();
            }

            return true;
        }

        public async Task<bool> MarkAllAsReadAsync(int userId)
        {
            var unreadList = await _context.Notifications
                .Where(n => n.UserId == userId && !n.IsRead)
                .ToListAsync();

            if (!unreadList.Any()) return true;

            foreach (var item in unreadList)
            {
                item.IsRead = true;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteNotificationAsync(int id, int userId)
        {
            var item = await _context.Notifications
                .FirstOrDefaultAsync(n => n.Id == id && n.UserId == userId);

            if (item == null) return false;

            _context.Notifications.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Notification> CreateNotificationAsync(Notification item)
        {
            _context.Notifications.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
    }
}
