using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KanG.Data;
using KanG.Controller;
using KanG.Enums;
using KanG.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using KanG.Services.IService;

namespace KanG.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<AuthResponse?> LoginAsync(LoginRequest loginRequest)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == loginRequest.Username && u.PasswordHash == loginRequest.Password);

                if (user == null)
                {
                    return null;
                }

            var token = GenerateJwtToken(user);

            return new AuthResponse
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = (int)user.Role,
                AvatarUrl = null,
                Token = token
            };
        }

        public async Task<AuthResponse?> RegisterAsync(RegisterRequest registerRequest)
        {
            if (await _context.Users.AnyAsync(u => u.Username == registerRequest.Username))
            {
                return null; // Username already exists
            }

            var user = new User
            {
                Username = registerRequest.Username,
                PasswordHash = registerRequest.Password,
                Email = registerRequest.Email,
                Role = UserRole.User,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Auto-generate welcome notification for the newly registered user
            var welcomeNotification = new Notification
            {
                UserId = user.Id,
                Title = "ยินดีต้อนรับสู่ KanG! 🌿",
                Message = $"สวัสดีคุณ {user.Username} เริ่มต้นวางแผนท่องเที่ยวเมืองกาญจน์และบันทึกสถานที่โปรดของคุณได้เลย!",
                Type = NotificationType.TripReminder,
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };
            _context.Notifications.Add(welcomeNotification);
            await _context.SaveChangesAsync();

            var token = GenerateJwtToken(user);

            return new AuthResponse
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = (int)user.Role,
                AvatarUrl = null,
                Token = token
            };
        }

        public string GenerateJwtToken(User user)
        {
            var jwtKey = _configuration["Jwt:Key"] ?? "KanGSuperSecretKeyForJwtTokenAuth2026KanchanaburiTravelApp!";
            var jwtIssuer = _configuration["Jwt:Issuer"] ?? "KanGServer";
            var jwtAudience = _configuration["Jwt:Audience"] ?? "KanGClient";

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var roleName = user.Role == UserRole.Admin ? "Admin" : "User";

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                new Claim(ClaimTypes.Role, roleName)
            };

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
