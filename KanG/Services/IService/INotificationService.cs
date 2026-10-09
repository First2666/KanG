using KanG.Models;

namespace KanG.Services.IService
{
    public interface INotificationService
    {
        Task<IEnumerable<Notification>> GetUserNotificationsAsync(int userId, bool unreadOnly = false);
        Task<int> GetUnreadCountAsync(int userId);
        Task<Notification?> GetNotificationByIdAsync(int id, int userId);
        Task<bool> MarkAsReadAsync(int id, int userId);
        Task<bool> MarkAllAsReadAsync(int userId);
        Task<bool> DeleteNotificationAsync(int id, int userId);
        Task<Notification> CreateNotificationAsync(Notification item);
    }
}
