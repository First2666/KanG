using KanG.Models;

namespace KanG.Services.IService
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<Event?> GetEventByIdAsync(int id);
        Task<Event> CreateEventAsync(Event item);
        Task<bool> DeleteEventAsync(int id);
    }
}
