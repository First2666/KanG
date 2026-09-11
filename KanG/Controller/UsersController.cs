using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KanG.Data;
using KanG.Models;

namespace KanG.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase // คอนโทรลเลอร์จัดการข้อมูลผู้ใช้งาน
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        public class LoginDto
        {
            public string Username { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }

        public class RegisterDto
        {
            public string Username { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<User>> Login([FromBody] LoginDto login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == login.Username && u.PasswordHash == login.Password);
            if (user == null)
            {
                return Unauthorized(new { message = "ชื่อผู้ใช้หรือรหัสผ่านไม่ถูกต้อง" });
            }
            return Ok(user);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<User>> Register([FromBody] RegisterDto register)
        {
            if (await _context.Users.AnyAsync(u => u.Username == register.Username))
            {
                return BadRequest(new { message = "ชื่อผู้ใช้นี้มีอยู่ในระบบแล้ว" });
            }

            var user = new User
            {
                Username = register.Username,
                PasswordHash = register.Password, // Simplified for this project
                Email = register.Email,
                Role = KanG.Enums.UserRole.User,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Create default trip plan
            var tripPlan = new TripPlan
            {
                UserId = user.Id,
                PlanName = "แผนการเดินทางของฉัน",
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(1),
                MemberCount = 1,
                CreatedAt = DateTime.UtcNow
            };
            
            _context.TripPlans.Add(tripPlan);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users
                .OrderBy(u => u.Id)
                .ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var item = await _context.Users.FindAsync(id);
            if (item == null) return NotFound();
            return item;
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
