using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace KanG.Models
{
    public class Category  // หมวดหมู่สถานที่
    {
        public int Id { get; set; } 
        public string Name { get; set; } = string.Empty; 

        [JsonIgnore]
        public ICollection<PlaceCategory> PlaceCategories { get; set; } = new List<PlaceCategory>();
    }
}
