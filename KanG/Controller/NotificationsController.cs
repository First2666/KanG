using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KanG.Data;
using KanG.Models;

namespace KanG.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase // คอนโทรลเลอร์จัดการข้อมูลการแจ้งเตือน
    {
        private readonly AppDbContext _context;

        public NotificationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Notifications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotifications()
        {
            return await _context.Notifications.ToListAsync();
        }

        // GET: api/Notifications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Notification>> GetNotification(int id)
        {
            var item = await _context.Notifications.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }
        
        // POST: api/Notifications
        [HttpPost]
        public async Task<ActionResult<Notification>> PostNotification(Notification item)
        {
            _context.Notifications.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNotification), new { id = item.Id }, item);
        }
        
        // DELETE: api/Notifications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            var item = await _context.Notifications.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Notifications.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
