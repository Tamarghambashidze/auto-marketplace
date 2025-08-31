using AutoMapper;
using FinalProject.Entities;
using FinalProject.Dtos;
using FinalProject.Interfaces;

namespace FinalProject.Services
{
    public class MapUsersService : IMapUsersService
    {
        private readonly IMapper _mapper;
        public MapUsersService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public User MapFromUserDtoToUser<T>(T userDto) where T : BaseUserDto
        {
            var user = _mapper.Map<User>(userDto);
            user.UserDetails = _mapper.Map<UserDetails>(userDto.UserDetails);
            return user;
        }

        public T MapFromUserToUserDto<T>(User user) where T : BaseUserDto
        {
            var userDto = _mapper.Map<T>(user);
            userDto.UserDetails = _mapper.Map<UserDetailsDto>(user.UserDetails);
            return userDto;
        }
    }
}
