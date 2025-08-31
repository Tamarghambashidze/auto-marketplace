using FinalProject.Entities;
using FinalProject.Dtos;

namespace FinalProject.Interfaces
{
    public interface IMapUsersService
    {
        User MapFromUserDtoToUser<T>(T userDto) where T : BaseUserDto;
        T MapFromUserToUserDto<T>(User user) where T : BaseUserDto;
    }
}
