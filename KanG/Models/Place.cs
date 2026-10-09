using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KanG.Models
{
    [JsonDerivedType(typeof(Attraction), typeDiscriminator: "attraction")]
    [JsonDerivedType(typeof(Restaurant), typeDiscriminator: "restaurant")]
    [JsonDerivedType(typeof(Accommodation), typeDiscriminator: "accommodation")]
    public abstract class Place  
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int LocationId { get; set; }
        public Location? Location { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public TimeSpan? OpeningTime { get; set; }
        public TimeSpan? ClosingTime { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        //ส่วนกลาง
        public ICollection<PlaceImage> Images { get; set; } = new List<PlaceImage>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
        public ICollection<PlaceTag> Tags { get; set; } = new List<PlaceTag>();
        public ICollection<PlaceCategory> Categories { get; set; } = new List<PlaceCategory>();
        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
