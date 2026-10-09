using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KanG.Models;
using KanG.Services.IService;

namespace KanG.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase // คอนโทรลเลอร์จัดการข้อมูลรายการโปรด
    {
        private readonly IFavoriteService _favoriteService;

        public FavoritesController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        [HttpGet("User/{userId}")]
        public async Task<ActionResult<IEnumerable<Favorite>>> GetUserFavorites(int userId)
        {
            var favorites = await _favoriteService.GetFavoritesByUserAsync(userId);
            return Ok(favorites);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Favorite>>> GetFavorites()
        {
            var favorites = await _favoriteService.GetAllFavoritesAsync();
            return Ok(favorites);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Favorite>> GetFavorite(int id)
        {
            var item = await _favoriteService.GetFavoriteByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }
        
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Favorite>> PostFavorite(Favorite item)
        {
            var result = await _favoriteService.AddFavoriteAsync(item);
            return CreatedAtAction(nameof(GetFavorite), new { id = result.Id }, result);
        }
        
        [Authorize]
        [HttpDelete("User/{userId}/Place/{placeId}")]
        public async Task<IActionResult> DeleteUserFavorite(int userId, int placeId)
        {
            var success = await _favoriteService.DeleteUserFavoriteAsync(userId, placeId);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavorite(int id)
        {
            var success = await _favoriteService.DeleteFavoriteAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
