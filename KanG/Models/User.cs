using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using KanG.Enums;

namespace KanG.Models
{
    public class User // ผู้ใช้งาน
    {
        public int Id { get; set; } 
        public string Username { get; set; } = string.Empty; 
        public string PasswordHash { get; set; } = string.Empty; 
        public string Email { get; set; } = string.Empty; 
        public UserRole Role { get; set; } = UserRole.User; 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 

        [JsonIgnore]
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
        [JsonIgnore]
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        [JsonIgnore]
        public ICollection<TripPlan> TripPlans { get; set; } = new List<TripPlan>();
        [JsonIgnore]
        public ICollection<UserActivityLog> ActivityLogs { get; set; } = new List<UserActivityLog>();
        [JsonIgnore]
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}

