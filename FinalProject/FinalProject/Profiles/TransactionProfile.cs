using AutoMapper;
using FinalProject.Dtos;
using FinalProject.Entities;

namespace FinalProject.Profiles
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<Transaction, TransactionDto>()
                .ForMember(dest => dest.BoughtCar, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
