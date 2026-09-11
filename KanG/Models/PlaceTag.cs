using System.Text.Json.Serialization;
namespace KanG.Models
{
    public class PlaceTag  // ตารางเชื่อมสถานที่กับแท็ก
    {
        public int Id { get; set; }

        public int PlaceId { get; set; }
        [JsonIgnore]
        public Place? Place { get; set; }

        public int TagId { get; set; }
        [JsonIgnore]
        public Tag? Tag { get; set; }
    }
}
