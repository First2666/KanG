using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;

namespace KanG.Models
{
    public class TripPlan // เนเธเธเธเธฒเธฃเน€เธ”เธดเธเธ—เธฒเธเธ—เธตเนเธเธนเนเนเธเนเธชเธฃเนเธฒเธ (เธกเธต BudgetAmount + MemberCount)
    {
        public int Id { get; set; } 
        public int UserId { get; set; } 
        [JsonIgnore]
        public User? User { get; set; }

        public string PlanName { get; set; } = string.Empty; 
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; } 

        public int MemberCount { get; set; } = 1; // เธเธณเธเธงเธเธเธนเนเธฃเนเธงเธกเน€เธ”เธดเธเธ—เธฒเธ

       
        public decimal? BudgetAmount { get; set; } // เธเธเธเธฃเธฐเธกเธฒเธ“เธฃเธงเธกเธ—เธตเนเธ•เธฑเนเธเนเธงเน

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 

        public ICollection<TripPlanItem> Items { get; set; } = new List<TripPlanItem>();
    }
}

