using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KanG.Services.IService;

namespace KanG.Controller
{
    public class PlaceRequest
    {
        public string PlaceType { get; set; } = "Attraction";
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int LocationId { get; set; } = 1;
        public decimal EntranceFee { get; set; } = 0;
        public TimeSpan? OpeningTime { get; set; }
        public TimeSpan? ClosingTime { get; set; }

        public List<int> CategoryIds { get; set; } = new List<int>();
        public List<int> TagIds { get; set; } = new List<int>();
    }

    [Route("api/[controller]")]
    [ApiController]
    public class PlacesController : ControllerBase // คอนโทรลเลอร์จัดการข้อมูลสถานที่ (หลัก)
    {
        private readonly IPlaceService _placeService;
        private readonly IPlaceImageService _placeImageService;

        public PlacesController(IPlaceService placeService, IPlaceImageService placeImageService)
        {
            _placeService = placeService;
            _placeImageService = placeImageService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetPlaces()
        {
            var places = await _placeService.GetAllPlacesAsync();
            return Ok(places);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetPlace(int id)
        {
            var place = await _placeService.GetPlaceByIdAsync(id);
            if (place == null) return NotFound();
            return Ok(place);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<object>> PostPlace([FromBody] PlaceRequest request)
        {
            var result = await _placeService.CreatePlaceAsync(request);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlace(int id, [FromBody] PlaceRequest request)
        {
            var success = await _placeService.UpdatePlaceAsync(id, request);
            if (!success) return NotFound();
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlace(int id)
        {
            var success = await _placeService.DeletePlaceAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }

        // Upload Images Endpoint
        [Authorize(Roles = "Admin")]
        [HttpPost("{id}/Images")]
        public async Task<IActionResult> UploadImage(int id, IFormFile file)
        {
            if (file == null || file.Length == 0) return BadRequest(new { message = "No file uploaded" });

            var image = await _placeService.UploadImageAsync(id, file);
            if (image == null) return NotFound(new { message = "Place not found" });

            return Ok(image);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}/Images/{imageId}")]
        public async Task<IActionResult> DeleteImage(int id, int imageId)
        {
            var success = await _placeImageService.DeletePlaceImageAsync(imageId);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
