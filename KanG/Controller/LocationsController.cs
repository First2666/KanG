using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KanG.Models;
using KanG.Services.IService;

namespace KanG.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase // คอนโทรลเลอร์จัดการข้อมูลพิกัดตำแหน่ง
    {
        private readonly ILocationService _locationService;

        public LocationsController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Location>>> GetLocations()
        {
            var locations = await _locationService.GetAllLocationsAsync();
            return Ok(locations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Location>> GetLocation(int id)
        {
            var item = await _locationService.GetLocationByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Location>> PostLocation(Location item)
        {
            var created = await _locationService.CreateLocationAsync(item);
            return CreatedAtAction(nameof(GetLocation), new { id = created.Id }, created);
        }
        
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            var success = await _locationService.DeleteLocationAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
