using KanG.Models;

namespace KanG.Services.IService
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<bool> DeleteUserAsync(int id);
    }
}
