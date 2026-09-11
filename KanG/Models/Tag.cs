using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace KanG.Models
{
    public class Tag  // ป้ายกำกับสถานที่
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; 

        public ICollection<PlaceTag> PlaceTags { get; set; } = new List<PlaceTag>();
    }
}
