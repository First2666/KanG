using Microsoft.EntityFrameworkCore;
using KanG.Models;
using System.Reflection.Emit;

namespace KanG.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Attraction> Attractions { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Accommodation> Accommodations { get; set; }

        public DbSet<PlaceCategory> PlaceCategories { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Location> Locations { get; set; }

        public DbSet<PlaceImage> PlaceImages { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Favorite> Favorites { get; set; }

        public DbSet<TripPlan> TripPlans { get; set; }
        public DbSet<TripPlanItem> TripPlanItems { get; set; }

        public DbSet<Tag> Tags { get; set; }
        public DbSet<PlaceTag> PlaceTags { get; set; }

        public DbSet<UserActivityLog> UserActivityLogs { get; set; }
        public DbSet<UserActivityLogTag> UserActivityLogTags { get; set; }

        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<AttractionDistance> AttractionDistances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Place>()
                .HasDiscriminator<string>("PlaceType")
                .HasValue<Attraction>("Attraction")
                .HasValue<Restaurant>("Restaurant")
                .HasValue<Accommodation>("Accommodation");

            modelBuilder.Entity<Place>()
                .HasOne(p => p.Location).WithMany(l => l.Places)
                .HasForeignKey(p => p.LocationId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PlaceCategory>()
                .HasOne(ac => ac.Place).WithMany(a => a.Categories)
                .HasForeignKey(ac => ac.PlaceId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<PlaceCategory>()
                .HasOne(ac => ac.Category).WithMany(c => c.PlaceCategories)
                .HasForeignKey(ac => ac.CategoryId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<PlaceCategory>()
                .HasIndex(ac => new { ac.PlaceId, ac.CategoryId }).IsUnique();

            modelBuilder.Entity<PlaceImage>()
                .HasOne(pi => pi.Place).WithMany(p => p.Images)
                .HasForeignKey(pi => pi.PlaceId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.User).WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Place).WithMany(p => p.Reviews)
                .HasForeignKey(r => r.PlaceId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.User).WithMany(u => u.Favorites)
                .HasForeignKey(f => f.UserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.Place).WithMany(p => p.Favorites)
                .HasForeignKey(f => f.PlaceId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Favorite>()
                .HasIndex(f => new { f.UserId, f.PlaceId }).IsUnique();

            modelBuilder.Entity<TripPlan>()
                .HasOne(t => t.User).WithMany(u => u.TripPlans)
                .HasForeignKey(t => t.UserId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TripPlanItem>()
                .HasOne(t => t.TripPlan).WithMany(tp => tp.Items)
                .HasForeignKey(t => t.TripPlanId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<TripPlanItem>()
                .HasOne(t => t.Place).WithMany()
                .HasForeignKey(t => t.PlaceId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AttractionDistance>()
                .HasOne(a => a.SourceAttraction).WithMany()
                .HasForeignKey(a => a.SourceAttractionId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<AttractionDistance>()
                .HasOne(a => a.DestinationAttraction).WithMany()
                .HasForeignKey(a => a.DestinationAttractionId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<AttractionDistance>()
                .HasIndex(a => new { a.SourceAttractionId, a.DestinationAttractionId }).IsUnique();

            modelBuilder.Entity<UserActivityLog>()
                .HasOne(u => u.User).WithMany(u => u.ActivityLogs)
                .HasForeignKey(u => u.UserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UserActivityLog>()
                .HasOne(u => u.RelatedPlace).WithMany()
                .HasForeignKey(u => u.RelatedPlaceId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PlaceTag>()
                .HasOne(at => at.Place).WithMany(a => a.Tags)
                .HasForeignKey(at => at.PlaceId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<PlaceTag>()
                .HasOne(at => at.Tag).WithMany(t => t.PlaceTags)
                .HasForeignKey(at => at.TagId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<PlaceTag>()
                .HasIndex(at => new { at.PlaceId, at.TagId }).IsUnique();

            modelBuilder.Entity<UserActivityLogTag>()
                .HasOne(u => u.UserActivityLog).WithMany(l => l.Tags)
                .HasForeignKey(u => u.UserActivityLogId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UserActivityLogTag>()
                .HasIndex(u => new { u.UserActivityLogId, u.TagId }).IsUnique();

            modelBuilder.Entity<Event>()
                .HasOne(e => e.Place).WithMany(a => a.Events)
                .HasForeignKey(e => e.PlaceId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User).WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.RelatedTripPlan).WithMany()
                .HasForeignKey(n => n.RelatedTripPlanId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.RelatedEvent).WithMany()
                .HasForeignKey(n => n.RelatedEventId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.RelatedPlace).WithMany()
                .HasForeignKey(n => n.RelatedPlaceId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
