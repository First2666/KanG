using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KanG.Models;
using KanG.Services.IService;

namespace KanG.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceImagesController : ControllerBase // คอนโทรลเลอร์จัดการข้อมูลรูปภาพสถานที่
    {
        private readonly IPlaceImageService _placeImageService;

        public PlaceImagesController(IPlaceImageService placeImageService)
        {
            _placeImageService = placeImageService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlaceImage>>> GetPlaceImages()
        {
            var images = await _placeImageService.GetAllPlaceImagesAsync();
            return Ok(images);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlaceImage>> GetPlaceImage(int id)
        {
            var item = await _placeImageService.GetPlaceImageByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<PlaceImage>> PostPlaceImage(PlaceImage item)
        {
            var created = await _placeImageService.CreatePlaceImageAsync(item);
            return CreatedAtAction(nameof(GetPlaceImage), new { id = created.Id }, created);
        }
        
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlaceImage(int id)
        {
            var success = await _placeImageService.DeletePlaceImageAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
