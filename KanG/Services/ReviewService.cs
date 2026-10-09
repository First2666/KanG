using KanG.Data;
using KanG.Models;
using KanG.Services.IService;
using Microsoft.EntityFrameworkCore;

namespace KanG.Services
{
    public class ReviewService : IReviewService
    {
        private readonly AppDbContext _context;

        public ReviewService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Review>> GetAllReviewsAsync()
        {
            return await _context.Reviews.ToListAsync();
        }

        public async Task<Review?> GetReviewByIdAsync(int id)
        {
            return await _context.Reviews.FindAsync(id);
        }

        public async Task<Review> CreateReviewAsync(Review review)
        {
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task<bool> DeleteReviewAsync(int id)
        {
            var item = await _context.Reviews.FindAsync(id);
            if (item == null) return false;

            _context.Reviews.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
