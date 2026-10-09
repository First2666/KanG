using Microsoft.AspNetCore.Mvc;
using KanG.Models;
using KanG.Services.IService;

namespace KanG.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttractionsController : ControllerBase // คอนโทรลเลอร์จัดการข้อมูลสถานที่ท่องเที่ยว
    {
        private readonly IPlaceService _placeService;

        public AttractionsController(IPlaceService placeService)
        {
            _placeService = placeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attraction>>> GetAttractions()
        {
            var attractions = await _placeService.GetAttractionsAsync();
            return Ok(attractions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Attraction>> GetAttraction(int id)
        {
            var attraction = await _placeService.GetAttractionByIdAsync(id);
            if (attraction == null)
            {
                return NotFound();
            }
            return Ok(attraction);
        }
    }
}
