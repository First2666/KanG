using KanG.Enums;
using KanG.Models;

namespace KanG.Data
{
    public static class DbInitializer
    {
        public static void SeedNotifications(AppDbContext context)
        {
            var users = context.Users.ToList();
            if (!users.Any()) return;

            var firstTrip = context.TripPlans.FirstOrDefault();
            var firstPlace = context.Places.FirstOrDefault();

            foreach (var user in users)
            {
                if (!context.Notifications.Any(n => n.UserId == user.Id))
                {
                    context.Notifications.AddRange(
                        new Notification
                        {
                            UserId = user.Id,
                            Type = NotificationType.TripReminder,
                            Title = "ยินดีต้อนรับสู่ KanG! 🌿",
                            Message = "ยินดีต้อนรับสู่ระบบแนะนำและวางแผนท่องเที่ยวเมืองกาญจนบุรี เริ่มต้นสำรวจสถานที่ยอดนิยมและจัดทริปได้เลย",
                            RelatedTripPlanId = firstTrip?.Id,
                            IsRead = false,
                            CreatedAt = DateTime.UtcNow.AddMinutes(-25)
                        },
                        new Notification
                        {
                            UserId = user.Id,
                            Type = NotificationType.NewEvent,
                            Title = "งานสัปดาห์สะพานข้ามแม่น้ำแคว 2026 🎪",
                            Message = "ชมการแสดงแสง สี เสียง สุดตระการตา ย้อนรอยประวัติศาสตร์สงครามโลกครั้งที่ 2 เริ่มต้นสัปดาห์นี้แล้ว!",
                            RelatedPlaceId = firstPlace?.Id,
                            IsRead = false,
                            CreatedAt = DateTime.UtcNow.AddHours(-3)
                        },
                        new Notification
                        {
                            UserId = user.Id,
                            Type = NotificationType.BudgetAlert,
                            Title = "แจ้งเตือนงบประมาณทริป 💰",
                            Message = "ยอดรวมค่าใช้จ่ายในแผนการเดินทางของคุณมียอดรวม 4,500 บาท ใกล้เคียงงบประมาณที่ตั้งไว้ (5,000 บาท)",
                            RelatedTripPlanId = firstTrip?.Id,
                            IsRead = false,
                            CreatedAt = DateTime.UtcNow.AddHours(-8)
                        },
                        new Notification
                        {
                            UserId = user.Id,
                            Type = NotificationType.CrowdAlert,
                            Title = "แจ้งเตือนความหนาแน่น: น้ำตกเอราวัณ 👥",
                            Message = "ขณะนี้น้ำตกเอราวัณมีนักท่องเที่ยวหนาแน่น แนะนำวางแผนเดินทางเข้าชมช่วงเช้า 08:00 - 10:00 น.",
                            IsRead = true,
                            CreatedAt = DateTime.UtcNow.AddDays(-1)
                        },
                        new Notification
                        {
                            UserId = user.Id,
                            Type = NotificationType.TripReminder,
                            Title = "ใกล้ถึงวันออกเดินทางแล้ว 🎒",
                            Message = "เหลืออีก 2 วันสำหรับทริปเที่ยวกาญจนบุรีของคุณ อย่าลืมเช็คสภาพอากาศและเตรียมกล้องถ่ายรูปให้พร้อมนะ",
                            RelatedTripPlanId = firstTrip?.Id,
                            IsRead = true,
                            CreatedAt = DateTime.UtcNow.AddDays(-2)
                        }
                    );
                }
            }

            context.SaveChanges();
        }
    }
}
