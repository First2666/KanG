using System.Text.Json.Serialization;
using System;

namespace KanG.Models
{
    public class PlaceImage  // รูปภาพของสถานที่
    {
        public int Id { get; set; } 
        public string ImageUrl { get; set; } = string.Empty; 
        public string Description { get; set; } = string.Empty; 
        public bool IsPrimary { get; set; } 

        public int PlaceId { get; set; } 
        [JsonIgnore]
        public Place? Place { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
    }
}
