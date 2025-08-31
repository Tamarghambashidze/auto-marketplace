using FinalProject.Extensions;
using FinalProject.Dtos;
using FinalProject.Exceptions;
using FinalProject.Interfaces;

namespace FinalProject.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapUsersService _mapUsersService;
        public UserService(IUserRepository userRepository, IMapUsersService mapUsersService)
        {
            _userRepository = userRepository;
            _mapUsersService = mapUsersService;
        }

        public async Task AddUserAsync(CreateUserDto userDto)
        {
            var user = _mapUsersService.MapFromUserDtoToUser(userDto);
            user.PasswordHash = userDto.PasswordHash.GetPasswordHash();
            await _userRepository.AddUserAsync(user);
        }

        public async Task<UserDto> FindUserAsync(UserLogIn userLogIn)
        {
            var user = await _userRepository.GetUserByEmail(userLogIn.Email);
            if (!userLogIn.PasswordHash.VerifyPassword(user.PasswordHash))
                throw new UserException("Invalid password");
            return _mapUsersService.MapFromUserToUserDto<UserDto>(user);
        }

        public async Task UpdateUserAsync(int id, BaseUserDto userDto)
        {
            var user = _mapUsersService.MapFromUserDtoToUser(userDto);
            await _userRepository.UpdateUserAsync(id, user);
        }

        public async Task UpdatePasswordAsync(UserPasswordUpdate userPasswordUpdate)
        {
            var user = await FindUserAsync(new UserLogIn()
            {
                Email = userPasswordUpdate.Email, 
                PasswordHash = userPasswordUpdate.OldPassword
            });
            await _userRepository.UpdateUserPasswordAsync(user.Id, userPasswordUpdate.NewPassword.GetPasswordHash());
        }

        public async Task DeleteUserAsync(int id, UserLogIn userLogIn)
        {
            await FindUserAsync(userLogIn);
            await _userRepository.DeleteUser(id);
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserById(id);
            return _mapUsersService.MapFromUserToUserDto<UserDto>(user);
        }
    }
}
