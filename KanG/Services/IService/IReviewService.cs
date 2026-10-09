using KanG.Models;

namespace KanG.Services.IService
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetAllReviewsAsync();
        Task<Review?> GetReviewByIdAsync(int id);
        Task<Review> CreateReviewAsync(Review review);
        Task<bool> DeleteReviewAsync(int id);
    }
}
