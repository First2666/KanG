using KanG.Data;
using KanG.Models;
using KanG.Services.IService;
using Microsoft.EntityFrameworkCore;

namespace KanG.Services
{
    public class TripPlanService : ITripPlanService
    {
        private readonly AppDbContext _context;

        public TripPlanService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TripPlan>> GetTripPlansByUserAsync(int userId)
        {
            return await _context.TripPlans
                .Include(tp => tp.Items)
                .Where(tp => tp.UserId == userId)
                .OrderByDescending(tp => tp.StartDate)
                .ToListAsync();
        }

        public async Task<TripPlan?> GetTripPlanByIdAsync(int id)
        {
            return await _context.TripPlans
                .Include(tp => tp.Items)
                    .ThenInclude(i => i.Place)
                        .ThenInclude(p => p!.Location)
                .Include(tp => tp.Items)
                    .ThenInclude(i => i.Place)
                        .ThenInclude(p => p!.Images)
                .FirstOrDefaultAsync(tp => tp.Id == id);
        }

        public async Task<TripPlan> CreateTripPlanAsync(TripPlan tripPlan)
        {
            _context.TripPlans.Add(tripPlan);
            await _context.SaveChangesAsync();

            // Auto-trigger notification for user
            _context.Notifications.Add(new Notification
            {
                UserId = tripPlan.UserId,
                Type = KanG.Enums.NotificationType.TripReminder,
                Title = "สร้างแผนการเดินทางสำเร็จ 🎒",
                Message = $"คุณได้สร้างแผนการเดินทาง '{tripPlan.PlanName}' เรียบร้อยแล้ว เริ่มต้นเพิ่มสถานที่และคำนวณงบประมาณได้เลย",
                RelatedTripPlanId = tripPlan.Id,
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            });
            await _context.SaveChangesAsync();

            return tripPlan;
        }

        public async Task<bool> UpdateTripPlanAsync(int id, TripPlan tripPlan)
        {
            if (id != tripPlan.Id) return false;

            _context.Entry(tripPlan).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.TripPlans.AnyAsync(e => e.Id == id))
                {
                    return false;
                }
                throw;
            }
        }

        public async Task<bool> DeleteTripPlanAsync(int id)
        {
            var tripPlan = await _context.TripPlans.FindAsync(id);
            if (tripPlan == null) return false;

            _context.TripPlans.Remove(tripPlan);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TripPlanItem>> GetAllTripPlanItemsAsync()
        {
            return await _context.TripPlanItems.ToListAsync();
        }

        public async Task<TripPlanItem?> GetTripPlanItemByIdAsync(int id)
        {
            return await _context.TripPlanItems.FindAsync(id);
        }

        public async Task<TripPlanItem> CreateTripPlanItemAsync(TripPlanItem item)
        {
            _context.TripPlanItems.Add(item);
            await _context.SaveChangesAsync();

            var tripPlan = await _context.TripPlans.FindAsync(item.TripPlanId);
            var place = await _context.Places.FindAsync(item.PlaceId);
            if (tripPlan != null)
            {
                var placeName = place?.Name ?? "สถานที่ท่องเที่ยว";
                _context.Notifications.Add(new Notification
                {
                    UserId = tripPlan.UserId,
                    Type = KanG.Enums.NotificationType.TripReminder,
                    Title = "เพิ่มสถานที่ในแผนเดินทางแล้ว 📍",
                    Message = $"เพิ่ม '{placeName}' ลงในแผนการเดินทาง '{tripPlan.PlanName}' เรียบร้อยแล้ว",
                    RelatedTripPlanId = tripPlan.Id,
                    RelatedPlaceId = item.PlaceId,
                    IsRead = false,
                    CreatedAt = DateTime.UtcNow
                });
                await _context.SaveChangesAsync();
            }

            return item;
        }

        public async Task<bool> UpdateTripPlanItemAsync(int id, TripPlanItem item)
        {
            if (id != item.Id) return false;

            _context.Entry(item).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.TripPlanItems.AnyAsync(e => e.Id == id))
                {
                    return false;
                }
                throw;
            }
        }

        public async Task<bool> DeleteTripPlanItemAsync(int id)
        {
            var item = await _context.TripPlanItems.FindAsync(id);
            if (item == null) return false;

            _context.TripPlanItems.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
