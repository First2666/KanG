using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace KanG.Models
{
    public class Location  // พิกัดตำแหน่งที่ตั้ง
    {
        public int Id { get; set; } 
        public double Latitude { get; set; } 
        public double Longitude { get; set; } 
        public string Address { get; set; } = string.Empty; 
        public string PhoneNumber { get; set; } = string.Empty; 
        public string Website { get; set; } = string.Empty; 

        [JsonIgnore]
        public ICollection<Place> Places { get; set; } = new List<Place>();
    }
}
