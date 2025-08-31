using FinalProject.Dtos;

namespace FinalProject.Interfaces
{
    public interface IUserService
    {
        Task AddUserAsync(CreateUserDto userDto);
        Task<UserDto> FindUserAsync(UserLogIn userLogIn);
        Task UpdateUserAsync(int id, BaseUserDto userDto);
        Task UpdatePasswordAsync(UserPasswordUpdate userPasswordUpdate);
        Task DeleteUserAsync(int id, UserLogIn userLogIn);
        Task<UserDto> GetUserByIdAsync(int id);
    }
}
