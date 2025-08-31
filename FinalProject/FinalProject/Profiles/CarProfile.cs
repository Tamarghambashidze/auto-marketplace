using AutoMapper;
using FinalProject.Entities;
using FinalProject.Dtos;

namespace FinalProject.Mappers
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {

            CreateMap<CarDto, Car>()
                .ForMember(dest => dest.Manufacturer, opt => opt.Ignore())
                .ForMember(dest => dest.Details, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<BaseCarDto, Car>()
                .ForMember(dest => dest.Manufacturer, opt => opt.Ignore())
                .ForMember(dest => dest.Details, opt => opt.Ignore());

            CreateMap<CarDetailsDto, CarDetails>()
                .ForMember(dest => dest.FuelType, opt => opt.Ignore())
                .ForMember(dest => dest.Transmission, opt => opt.Ignore())
                .ForMember(dest => dest.DriveTrain, opt => opt.Ignore())
                .ReverseMap();

        }
    }
}



