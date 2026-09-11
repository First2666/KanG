using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;

namespace KanG.Models
{
    public class Event  // กิจกรรมหรือเทศกาล
    {
        public int Id { get; set; } 
        public string Name { get; set; } = string.Empty; 
        public string Description { get; set; } = string.Empty; 

        public int PlaceId { get; set; } 
        [JsonIgnore]
        public Place? Place { get; set; }

        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; } 

        public string ImageUrl { get; set; } = string.Empty;

        public bool IsUpcoming => EndDate >= DateTime.UtcNow;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
    }
}
