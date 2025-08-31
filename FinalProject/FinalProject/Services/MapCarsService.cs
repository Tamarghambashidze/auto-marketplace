using AutoMapper;
using FinalProject.Entities;
using FinalProject.Dtos;
using FinalProject.Interfaces;

namespace FinalProject.Services
{
    public class MapCarsService : IMapCarsService
    {
        private readonly ICarDetailsRepository _carDetailsRepository;
        private readonly IMapper _mapper;
        public MapCarsService(ICarDetailsRepository carDetailsRepository, IMapper mapper)
        {
            _carDetailsRepository = carDetailsRepository;
            _mapper = mapper;
        }
        public async Task<Car> MapFromCarDtoToCar<T>(T carDto) where T : BaseCarDto
        {
            var manufacturer = await _carDetailsRepository.GetByNameAsync(carDto.Manufacturer);
            var car = _mapper.Map<Car>(carDto);
            car.ManufacturerId = manufacturer.Id;
            car.Manufacturer = manufacturer;
            car.Details = await MapFromCarDetailsDtoToCarDetails(car.Id, carDto.Details);
            return car;
        }
        public CarDto MapFromCarToCarDto(Car car)
        {
            var carDto = _mapper.Map<CarDto>(car);
            carDto.Manufacturer = car.Manufacturer.Name;
            carDto.Details = MapFromCarDetailsToCarDetailsDto(car.Details);
            return carDto;
        }

        public async Task<CarDetails> MapFromCarDetailsDtoToCarDetails(int carId, CarDetailsDto carDetailsDto)
        {
            var transmission = await _carDetailsRepository.GetTransmissionAsync(carDetailsDto.Transmission);
            var driveTrain = await _carDetailsRepository.GetDriveTrainAsync(carDetailsDto.DriveTrain);
            var fuelType = await _carDetailsRepository.GetFuelTypeAsync(carDetailsDto.FuelType);

            var details = _mapper.Map<CarDetails>(carDetailsDto);
            details.FuelTypeId = fuelType.Id;
            details.FuelType = fuelType;
            details.TransmissionId = transmission.Id;
            details.Transmission = transmission;
            details.DrivetrainId = driveTrain.Id;
            details.DriveTrain = driveTrain;
            details.CarId = carId;
            return details;
        }
        public CarDetailsDto MapFromCarDetailsToCarDetailsDto(CarDetails carDetails)
        {
            var carDetailsDto = _mapper.Map<CarDetailsDto>(carDetails);
            carDetailsDto.FuelType = carDetails.FuelType.Name;
            carDetailsDto.Transmission = carDetails.Transmission.Name;
            carDetailsDto.DriveTrain = carDetails.DriveTrain.Name;
            return carDetailsDto;
        }
    }
}
