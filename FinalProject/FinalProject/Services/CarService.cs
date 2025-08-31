using FinalProject.Entities;
using FinalProject.Dtos;
using FinalProject.Interfaces;
using FinalProject.Exceptions;
using FinalProject.Extensions;

namespace FinalProject.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapCarsService _mapCarsService;
        public CarService(ICarRepository carRepository, IMapCarsService mapCarsService)
        {
            _carRepository = carRepository;
            _mapCarsService = mapCarsService;
        }

        public async Task AddCarAsync(BaseCarDto carDto)
        {
            var car = await _mapCarsService.MapFromCarDtoToCar(carDto);
            var newCar = await _carRepository.AddCarAsync(car);
            var details = await _mapCarsService.MapFromCarDetailsDtoToCarDetails(car.Id, carDto.Details);
            await _carRepository.AddCarDetailsAsync(details);
        }
        public async Task<List<CarDto>> GetAllCars()
        {
            var cars = await _carRepository.GetAllCars();
            var carDtos = cars.Select(c => _mapCarsService.MapFromCarToCarDto(c)).ToList();
            return carDtos;
        }

        public async Task<CarDto> GetCarById(int id)
        {
            var car = await _carRepository.GetByIdAsync(id);
            if (car.IsSold)
                throw new BuyException("Car is already sold");
            return _mapCarsService.MapFromCarToCarDto(car);
        }
        
        public async Task<List<CarDto>> SearchCar(string value)
        {
            var cars = await _carRepository.SearchCar(value);
            var result = cars.Select(c => _mapCarsService.MapFromCarToCarDto(c)).ToList();
            return result;
        }

        public async Task<CarDto> UpdateCarAsync(int id, BaseCarDto carDto)
        {
            var car = await _mapCarsService.MapFromCarDtoToCar(carDto);
            var newCar = await _carRepository.UpdateCarAsync(id, car);
            await _carRepository.UpdateCarDetails(id, newCar.Details, newCar);
            return _mapCarsService.MapFromCarToCarDto(newCar);
        } 

        public async Task DeleteCarAsync(int id)
        {
            await _carRepository.DeleteCarAsync(id);
        }

        public async Task<List<CarDto>> GetCarsPaginated(int pageNumber, int pageSize)
        {
            var cars = await _carRepository.GetCarsPaginated(pageNumber, pageSize);
            return cars.Select(c => _mapCarsService.MapFromCarToCarDto(c)).ToList();
        }
    }
}

