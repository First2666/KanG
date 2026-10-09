using KanG.Data;
using KanG.Models;
using KanG.Services.IService;
using Microsoft.EntityFrameworkCore;

namespace KanG.Services
{
    public class EventService : IEventService
    {
        private readonly AppDbContext _context;

        public EventService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _context.Events.ToListAsync();
        }

        public async Task<Event?> GetEventByIdAsync(int id)
        {
            return await _context.Events.FindAsync(id);
        }

        public async Task<Event> CreateEventAsync(Event item)
        {
            _context.Events.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> DeleteEventAsync(int id)
        {
            var item = await _context.Events.FindAsync(id);
            if (item == null) return false;

            _context.Events.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
