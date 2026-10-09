using KanG.Models;

namespace KanG.Services.IService
{
    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class RegisterRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class AuthResponse
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string? Email { get; set; }
        public int Role { get; set; }
        public string? AvatarUrl { get; set; }
        public string Token { get; set; } = string.Empty;
    }

    public interface IAuthService
    {
        Task<AuthResponse?> LoginAsync(LoginRequest loginRequest);
        Task<AuthResponse?> RegisterAsync(RegisterRequest registerRequest);
        string GenerateJwtToken(User user);
    }
}
