using System;
using System.Collections.Generic;
using KanG.Enums;

namespace KanG.Models
{
    public class Restaurant : Place // ร้านอาหาร
    {
        public FoodType FoodType { get; set; } 
    }
}
