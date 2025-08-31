using FinalProject.Entities;

namespace FinalProject.Interfaces
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user);
        Task<User> GetUserByEmail(string email);
        Task UpdateUserAsync(int id, User user);
        Task UpdateUserPasswordAsync(int id, string passwordHash);
        Task DeleteUser(int id);
        Task<User> GetUserById(int id);
    }
}
