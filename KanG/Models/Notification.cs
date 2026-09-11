using System.Text.Json.Serialization;
using System;
using KanG.Enums;

namespace KanG.Models
{
    public class Notification  // การแจ้งเตือน
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        [JsonIgnore]
        public User? User { get; set; }

        public NotificationType Type { get; set; } 

        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;

        public int? RelatedTripPlanId { get; set; }
        public TripPlan? RelatedTripPlan { get; set; }

        public int? RelatedEventId { get; set; }
        public Event? RelatedEvent { get; set; }

        public int? RelatedPlaceId { get; set; }
        public Place? RelatedPlace { get; set; }

        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
