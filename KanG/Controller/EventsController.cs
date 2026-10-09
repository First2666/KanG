using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KanG.Models;
using KanG.Services.IService;

namespace KanG.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase // คอนโทรลเลอร์จัดการข้อมูลกิจกรรมและเทศกาล
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            var events = await _eventService.GetAllEventsAsync();
            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            var item = await _eventService.GetEventByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Event>> PostEvent(Event item)
        {
            var created = await _eventService.CreateEventAsync(item);
            return CreatedAtAction(nameof(GetEvent), new { id = created.Id }, created);
        }
        
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var success = await _eventService.DeleteEventAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
