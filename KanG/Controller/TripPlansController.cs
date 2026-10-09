using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KanG.Models;
using KanG.Services;
using KanG.Services.IService;

namespace KanG.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripPlansController : ControllerBase // คอนโทรลเลอร์จัดการข้อมูลแผนการเดินทาง
    {
        private readonly ITripPlanService _tripPlanService;

        public TripPlansController(ITripPlanService tripPlanService)
        {
            _tripPlanService = tripPlanService;
        }

        [HttpGet("User/{userId}")]
        public async Task<ActionResult<IEnumerable<TripPlan>>> GetTripPlansByUser(int userId)
        {
            var plans = await _tripPlanService.GetTripPlansByUserAsync(userId);
            return Ok(plans);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TripPlan>> GetTripPlan(int id)
        {
            var tripPlan = await _tripPlanService.GetTripPlanByIdAsync(id);
            if (tripPlan == null)
            {
                return NotFound();
            }
            return Ok(tripPlan);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<TripPlan>> PostTripPlan(TripPlan tripPlan)
        {
            var created = await _tripPlanService.CreateTripPlanAsync(tripPlan);
            return CreatedAtAction("GetTripPlan", new { id = created.Id }, created);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTripPlan(int id, TripPlan tripPlan)
        {
            var success = await _tripPlanService.UpdateTripPlanAsync(id, tripPlan);
            if (!success)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTripPlan(int id)
        {
            var success = await _tripPlanService.DeleteTripPlanAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
