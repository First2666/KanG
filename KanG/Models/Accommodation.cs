using System;
using System.Collections.Generic;
using KanG.Enums;

namespace KanG.Models
{
    public class Accommodation : Place // ที่พัก
    {
        public AccommodationType AccommodationType { get; set; }
        public decimal PricePerNight { get; set; } 
        public int MaxGuests { get; set; } 
    }
}
