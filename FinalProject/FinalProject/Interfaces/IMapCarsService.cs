using FinalProject.Entities;
using FinalProject.Dtos;

namespace FinalProject.Interfaces
{
    public interface IMapCarsService
    {
        Task<Car> MapFromCarDtoToCar<T>(T carDto) where T : BaseCarDto;
        CarDto MapFromCarToCarDto(Car car);
        Task<CarDetails> MapFromCarDetailsDtoToCarDetails(int carId, CarDetailsDto carDetailsDto);
        CarDetailsDto MapFromCarDetailsToCarDetailsDto(CarDetails carDetails);
    }
}
