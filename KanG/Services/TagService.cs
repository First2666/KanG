using KanG.Data;
using KanG.Models;
using KanG.Services.IService;
using Microsoft.EntityFrameworkCore;

namespace KanG.Services
{
    public class TagService : ITagService
    {
        private readonly AppDbContext _context;

        public TagService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tag>> GetAllTagsAsync()
        {
            if (!await _context.Tags.AnyAsync())
            {
                _context.Tags.AddRange(
                    new Tag { Name = "วิวสวยถ่ายรูปปัง" },
                    new Tag { Name = "ที่จอดรถสะดวก" },
                    new Tag { Name = "บรรยากาศดีริมน้ำ" },
                    new Tag { Name = "เหมาะสำหรับครอบครัว" },
                    new Tag { Name = "มีมุมกาแฟ" },
                    new Tag { Name = "สัตว์เลี้ยงเข้าได้" },
                    new Tag { Name = "เปิดให้บริการทุกวัน" },
                    new Tag { Name = "มีWi-Fiฟรี" }
                );
                await _context.SaveChangesAsync();
            }
            return await _context.Tags.ToListAsync();
        }

        public async Task<Tag?> GetTagByIdAsync(int id)
        {
            return await _context.Tags.FindAsync(id);
        }

        public async Task<Tag> CreateTagAsync(Tag tag)
        {
            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();
            return tag;
        }

        public async Task<bool> DeleteTagAsync(int id)
        {
            var item = await _context.Tags.FindAsync(id);
            if (item == null) return false;

            _context.Tags.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
