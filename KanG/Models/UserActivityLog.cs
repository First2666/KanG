using System;
using System.Collections.Generic;
using KanG.Enums;

namespace KanG.Models
{
    public class UserActivityLog  // บันทึกกิจกรรมผู้ใช้
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }

        public ActivityType ActivityType { get; set; } 

        public int? RelatedPlaceId { get; set; }
        public Place? RelatedPlace { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public ICollection<UserActivityLogTag> Tags { get; set; } = new List<UserActivityLogTag>();
    }
}
