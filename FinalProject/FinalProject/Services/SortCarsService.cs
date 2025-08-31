using FinalProject.Entities;
using FinalProject.Services;
using FinalProject.Dtos;
using FinalProject.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Services
{
    public class SortCarsService : ISortCarsService
    {
        private readonly ISortCarsRepository _sortCarsRepository;
        private readonly IMapCarsService _carDetailsService;
        private readonly ICarService _carService;
        public SortCarsService(ISortCarsRepository sortCarsRepository, IMapCarsService carDetailsService, ICarService carService)
        {
            _sortCarsRepository = sortCarsRepository;
            _carDetailsService = carDetailsService;
            _carService = carService;
        }

        public async Task<List<CarDto>> SortCarsByManufacturer(string name)
        {
            var cars = await _sortCarsRepository.SortByManufacturer(name);
            return cars.Select(c => _carDetailsService.MapFromCarToCarDto(c)).ToList();
        }

        public async Task<List<CarDto>> SortByAscending()
        {
            var cars = await _carService.GetAllCars();
            return cars.OrderBy(c => c.Price).ToList();
        }

        public async Task<List<CarDto>> SortByDescending()
        {
            var cars = await _carService.GetAllCars();
            return cars.OrderByDescending(c => c.Price).ToList();
        }

        public async Task<List<CarDto>> SortByDrivetrain(string name)
        {
            var cars = await _sortCarsRepository.SortByDriveTrain(name);
            return cars.Select(c => _carDetailsService.MapFromCarToCarDto(c)).ToList();
        }

        public async Task<List<CarDto>> SortByFuelType(string name)
        {
            var cars = await _sortCarsRepository.SortByFuelType(name);
            return cars.Select(c => _carDetailsService.MapFromCarToCarDto(c)).ToList();
        }

        public async Task<List<CarDto>> SortByTransmission(string name)
        {
            var cars = await _sortCarsRepository.SortByTransmission(name);
            return cars.Select(c => _carDetailsService.MapFromCarToCarDto(c)).ToList();
        }
    }
}
