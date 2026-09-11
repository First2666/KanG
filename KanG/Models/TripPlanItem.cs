using System.Text.Json.Serialization;
using System;

namespace KanG.Models
{
    public class TripPlanItem  // รายการในแผนการเดินทาง
    {
        public int Id { get; set; } 
        public int TripPlanId { get; set; } 
        [JsonIgnore]
        public TripPlan? TripPlan { get; set; }

        public int PlaceId { get; set; } 
        public Place? Place { get; set; }

        public int DayNumber { get; set; }   
        public int Sequence { get; set; }    

        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public decimal? EstimatedCost { get; set; }
    }
}
