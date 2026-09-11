using System.Text.Json.Serialization;
using System;

namespace KanG.Models
{
    public class Favorite  // รายการโปรด
    {
        public int Id { get; set; } 
        public int UserId { get; set; } 
        [JsonIgnore]
        public User? User { get; set; }

        public int PlaceId { get; set; } 
        [JsonIgnore]
        public Place? Place { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
    }
}
