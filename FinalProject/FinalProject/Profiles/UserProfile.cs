using AutoMapper;
using FinalProject.Entities;
using FinalProject.Dtos;
namespace FinalProject.Profiles
{
  public class UserProfile : Profile
  {
        public UserProfile()
        {
            CreateMap<User, BaseUserDto>()
                .ForMember(dest => dest.UserDetails, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<User, CreateUserDto>()
                .ForMember(dest => dest.UserDetails, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UserDto, User>()
                .ForMember(dest => dest.UserDetails, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UserDetails, UserDetailsDto>().ReverseMap();
        }
  }
}
