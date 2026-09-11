namespace KanG.Models
{
    public class AttractionDistance // เก็บระยะทาง/เวลาระหว่างสถานที่ (ใช้คำนวณเส้นทาง)
    {
        public int Id { get; set; }
        
        public int SourceAttractionId { get; set; }
        public Attraction? SourceAttraction { get; set; }
        
        public int DestinationAttractionId { get; set; }
        public Attraction? DestinationAttraction { get; set; }

        public double DistanceKm { get; set; }
        public int EstimatedMinutes { get; set; }
    }
}
