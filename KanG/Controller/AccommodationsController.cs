using Microsoft.AspNetCore.Mvc;
using KanG.Models;
using KanG.Services.IService;

namespace KanG.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccommodationsController : ControllerBase // คอนโทรลเลอร์จัดการข้อมูลที่พัก
    {
        private readonly IPlaceService _placeService;

        public AccommodationsController(IPlaceService placeService)
        {
            _placeService = placeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Accommodation>>> GetAccommodations()
        {
            var accommodations = await _placeService.GetAccommodationsAsync();
            return Ok(accommodations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Accommodation>> GetAccommodation(int id)
        {
            var accommodation = await _placeService.GetAccommodationByIdAsync(id);
            if (accommodation == null)
            {
                return NotFound();
            }
            return Ok(accommodation);
        }
    }
}
