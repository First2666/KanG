using KanG.Data;
using KanG.Models;
using KanG.Services.IService;
using KanG.Services;
using Microsoft.EntityFrameworkCore;

namespace KanG.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            if (!await _context.Categories.AnyAsync())
            {
                _context.Categories.AddRange(
                    new Category { Name = "เธเธฃเธฃเธกเธเธฒเธ•เธด & เธ เธนเน€เธเธฒ" },
                    new Category { Name = "เธเธฃเธฐเธงเธฑเธ•เธดเธจเธฒเธชเธ•เธฃเน & เธชเธเธเธฃเธฒเธกเนเธฅเธ" },
                    new Category { Name = "เธเนเธณเธ•เธ & เธเธทเธเธเนเธฒ" },
                    new Category { Name = "เธเธฒเน€เธเน & เธฃเนเธฒเธเธญเธฒเธซเธฒเธฃเธฃเธดเธกเธเนเธณ" },
                    new Category { Name = "เธ—เธตเนเธเธฑเธเนเธเธฃเธดเธกเธเนเธณ & เธฃเธตเธชเธญเธฃเนเธ—" },
                    new Category { Name = "เธเธดเธเธเธฃเธฃเธก & เนเธญเธ”เน€เธงเธเน€เธเธญเธฃเน" }
                );
                await _context.SaveChangesAsync();
            }
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }
    }
}
