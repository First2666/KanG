using KanG.Models;

namespace KanG.Services.IService
{
    public interface ITripPlanService
    {
        Task<IEnumerable<TripPlan>> GetTripPlansByUserAsync(int userId);
        Task<TripPlan?> GetTripPlanByIdAsync(int id);
        Task<TripPlan> CreateTripPlanAsync(TripPlan tripPlan);
        Task<bool> UpdateTripPlanAsync(int id, TripPlan tripPlan);
        Task<bool> DeleteTripPlanAsync(int id);

        Task<IEnumerable<TripPlanItem>> GetAllTripPlanItemsAsync();
        Task<TripPlanItem?> GetTripPlanItemByIdAsync(int id);
        Task<TripPlanItem> CreateTripPlanItemAsync(TripPlanItem item);
        Task<bool> UpdateTripPlanItemAsync(int id, TripPlanItem item);
        Task<bool> DeleteTripPlanItemAsync(int id);
    }
}
