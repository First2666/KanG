using Microsoft.AspNetCore.Mvc;
using KanG.Models;
using KanG.Services.IService;

namespace KanG.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase // คอนโทรลเลอร์จัดการข้อมูลร้านอาหาร
    {
        private readonly IPlaceService _placeService;

        public RestaurantsController(IPlaceService placeService)
        {
            _placeService = placeService;
        }

        // GET: api/Restaurants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurants()
        {
            var restaurants = await _placeService.GetRestaurantsAsync();
            return Ok(restaurants);
        }

        // GET: api/Restaurants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant>> GetRestaurant(int id)
        {
            var restaurant = await _placeService.GetRestaurantByIdAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }
            return Ok(restaurant);
        }
    }
}
