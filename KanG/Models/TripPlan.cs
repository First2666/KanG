using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;

namespace KanG.Models
{
    public class TripPlan 
    {
        public int Id { get; set; } 
        public int UserId { get; set; } 
        [JsonIgnore]
        public User? User { get; set; }

        public string PlanName { get; set; } = string.Empty; 
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; } 

        public int MemberCount { get; set; } = 1; 
       
        public decimal? BudgetAmount { get; set; } 

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 

        public ICollection<TripPlanItem> Items { get; set; } = new List<TripPlanItem>();
    }
}

