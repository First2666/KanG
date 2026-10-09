using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KanG.Models;
using KanG.Services.IService;

namespace KanG.Controller
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase // คอนโทรลเลอร์จัดการข้อมูลการแจ้งเตือน (ความปลอดภัยระดับ Claims-based Auth)
    {
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        private int? GetCurrentUserId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null && int.TryParse(claim.Value, out int userId))
            {
                return userId;
            }
            return null;
        }

        // GET: api/Notifications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotifications([FromQuery] bool unreadOnly = false)
        {
            var userId = GetCurrentUserId();
            if (userId == null) return Unauthorized();

            var notifications = await _notificationService.GetUserNotificationsAsync(userId.Value, unreadOnly);
            return Ok(notifications);
        }

        // GET: api/Notifications/unread-count
        [HttpGet("unread-count")]
        public async Task<ActionResult<int>> GetUnreadCount()
        {
            var userId = GetCurrentUserId();
            if (userId == null) return Unauthorized();

            var count = await _notificationService.GetUnreadCountAsync(userId.Value);
            return Ok(count);
        }

        // GET: api/Notifications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Notification>> GetNotification(int id)
        {
            var userId = GetCurrentUserId();
            if (userId == null) return Unauthorized();

            var item = await _notificationService.GetNotificationByIdAsync(id, userId.Value);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        // PUT: api/Notifications/5/read
        [HttpPut("{id}/read")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var userId = GetCurrentUserId();
            if (userId == null) return Unauthorized();

            var success = await _notificationService.MarkAsReadAsync(id, userId.Value);
            if (!success)
            {
                return NotFound();
            }
            return Ok(new { message = "Notification marked as read" });
        }

        // PUT: api/Notifications/read-all
        [HttpPut("read-all")]
        public async Task<IActionResult> MarkAllAsRead()
        {
            var userId = GetCurrentUserId();
            if (userId == null) return Unauthorized();

            await _notificationService.MarkAllAsReadAsync(userId.Value);
            return Ok(new { message = "All notifications marked as read" });
        }

        // DELETE: api/Notifications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            var userId = GetCurrentUserId();
            if (userId == null) return Unauthorized();

            var success = await _notificationService.DeleteNotificationAsync(id, userId.Value);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        // POST: api/Notifications (Admin can send notification)
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Notification>> PostNotification(Notification item)
        {
            var created = await _notificationService.CreateNotificationAsync(item);
            return CreatedAtAction(nameof(GetNotification), new { id = created.Id }, created);
        }
    }
}
