using System.Text.Json.Serialization;
using System;

namespace KanG.Models
{
    public class Review  // รีวิวและคะแนน
    {
        public int Id { get; set; } 

        public int UserId { get; set; } 
        [JsonIgnore]
        public User? User { get; set; }

        public int PlaceId { get; set; } 
        [JsonIgnore]
        public Place? Place { get; set; }

        public int Rating { get; set; } 
        public string Comment { get; set; } = string.Empty; 
        public bool IsApproved { get; set; } = true; 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
    }
}
