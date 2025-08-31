using FinalProject.Data;
using FinalProject.Entities;
using FinalProject.Exceptions;
using FinalProject.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Repositories
{
    public class SortCarsRepository : ISortCarsRepository
    {
        private readonly ApplicationDbContext _context;
        public SortCarsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Car>> SortByManufacturer(string name)
        {
            var manufacturer = await _context.Manufacturers
                .Include(m => m.Cars)
                    .ThenInclude(c => c.Details)
                        .ThenInclude(d => d.Transmission)
                .Include(m => m.Cars)
                    .ThenInclude(c => c.Details)
                        .ThenInclude(d => d.DriveTrain)
                .Include(m => m.Cars)
                    .ThenInclude(c => c.Details)
                        .ThenInclude(d => d.FuelType)
                .FirstOrDefaultAsync(m => m.Name.ToLower() == name.ToLower());
            if (manufacturer == null)
                throw new SortExceptions($"Manufacturer not found with name {name}");
            var cars = manufacturer.Cars.Where(c => c.IsSold).ToList();
            if (cars.Count == 0)
                throw new SortExceptions($"No cars added in {manufacturer.Name}");
            return cars;
        }
        public async Task<List<Car>> SortByDriveTrain(string name)
        {
            var driveTrain = await _context.DriveTrains
                .Include(m => m.Cars)
                    .ThenInclude(c => c.Car)
                        .ThenInclude(d => d.Manufacturer)
                .Include(m => m.Cars)
                    .ThenInclude(c => c.FuelType)
                .Include(m => m.Cars)
                    .ThenInclude(c => c.Transmission)
                .FirstOrDefaultAsync(m => m.Name.ToLower() == name.ToLower());

            if (driveTrain == null)
                throw new SortExceptions($"Drivetrain not found with name: {name}");
            var cars = driveTrain.Cars.Where(c => !c.Car.IsSold);
            if (cars.Count() == 0)
                throw new SortExceptions($"No cars added in {driveTrain.Name}");
            return cars.Select(c => c.Car).ToList();
        }

        public async Task<List<Car>> SortByFuelType(string name)
        {
            var fuelType = await _context.FuelTypes
                .Include(m => m.Cars)
                    .ThenInclude(c => c.Car)
                        .ThenInclude(d => d.Manufacturer)
                .Include(m => m.Cars)
                    .ThenInclude(c => c.Transmission)
                .Include(m => m.Cars)
                    .ThenInclude(c => c.DriveTrain)
                .FirstOrDefaultAsync(m => m.Name.ToLower() == name.ToLower());
            if (fuelType == null)
                throw new SortExceptions($"Fuel type not found with name: {name}");
            var cars = fuelType.Cars.Where(c => !c.Car.IsSold);
            if (cars.Count() == 0)
                throw new SortExceptions($"No cars added in {fuelType.Name}");
            return cars.Select(c => c.Car).ToList();
        }

        public async Task<List<Car>> SortByTransmission(string name)
        {
            var transmission = await _context.Transmissions
                .Include(m => m.Cars)
                    .ThenInclude(c => c.Car)
                        .ThenInclude(d => d.Manufacturer)
                .Include(m => m.Cars)
                    .ThenInclude(c => c.FuelType)
                .Include(m => m.Cars)
                    .ThenInclude(c => c.DriveTrain)
                .FirstOrDefaultAsync(m => m.Name.ToLower() == name.ToLower());
            if (transmission == null)
                throw new SortExceptions($"Transmission not found with name: {name}");
            var cars = transmission.Cars.Where(c => !c.Car.IsSold);
            if (cars.Count() == 0)
                throw new SortExceptions($"No cars added in {transmission.Name}");
            return cars.Select(c => c.Car).ToList();
        }
    }
}
