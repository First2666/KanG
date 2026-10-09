using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KanG.Models;
using KanG.Services;
using KanG.Services.IService;

namespace KanG.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripPlanItemsController : ControllerBase // คอนโทรลเลอร์จัดการข้อมูลรายการในแผนทริป
    {
        private readonly ITripPlanService _tripPlanService;

        public TripPlanItemsController(ITripPlanService tripPlanService)
        {
            _tripPlanService = tripPlanService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TripPlanItem>>> GetTripPlanItems()
        {
            var items = await _tripPlanService.GetAllTripPlanItemsAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TripPlanItem>> GetTripPlanItem(int id)
        {
            var item = await _tripPlanService.GetTripPlanItemByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<TripPlanItem>> PostTripPlanItem(TripPlanItem item)
        {
            var created = await _tripPlanService.CreateTripPlanItemAsync(item);
            return CreatedAtAction(nameof(GetTripPlanItem), new { id = created.Id }, created);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTripPlanItem(int id, TripPlanItem item)
        {
            var success = await _tripPlanService.UpdateTripPlanItemAsync(id, item);
            if (!success)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTripPlanItem(int id)
        {
            var success = await _tripPlanService.DeleteTripPlanItemAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
