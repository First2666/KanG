using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace KanG.Models
{
    public class PlaceCategory  // ตารางเชื่อมสถานที่กับหมวดหมู่
    {
        public int Id { get; set; }

        public int PlaceId { get; set; }
        [JsonIgnore]
        public Place? Place { get; set; }

        public int CategoryId { get; set; }
        [JsonIgnore]
        public Category? Category { get; set; }
    }
}
