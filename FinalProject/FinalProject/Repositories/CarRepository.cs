using FinalProject.Data;
using FinalProject.Entities;
using FinalProject.Exceptions;
using FinalProject.Interfaces;
using FinalProject.Extensions;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext _context;

        public CarRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Car> AddCarAsync(Car car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
            return car;
        }

        public async Task<CarDetails> AddCarDetailsAsync(CarDetails carDetails)
        {
            await _context.CarDetails.AddAsync(carDetails);
            await _context.SaveChangesAsync();
            return carDetails;
        }

        public async Task<List<Car>> GetAllCars()
        {
            var cars = await _context.Cars
                .Include(c => c.Details)
                    .ThenInclude(d => d.Transmission)
                .Include(c => c.Details)
                    .ThenInclude(d => d.DriveTrain)
                .Include(c => c.Details)
                    .ThenInclude(d => d.FuelType)
                .Include(c => c.Manufacturer)
                .Where(c => !c.IsSold)
                .ToListAsync();
            if (!cars.Any())
                throw new CarExceptions("No cars added");
            return cars;
        }

        public async Task<Car> GetByIdAsync(int id)
        {
            var car = await _context.Cars
                .Include(c => c.Details)
                    .ThenInclude(d => d.Transmission)
                .Include(c => c.Details)
                    .ThenInclude(d => d.DriveTrain)
                .Include(c => c.Details)
                    .ThenInclude(d => d.FuelType)
                .Include(c => c.Manufacturer)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (car == null)
                throw new CarExceptions($"Car not found with id: {id}");
            return car;
        }

        public async Task<List<Car>> SearchCar(string value)
        {
            var result = await _context.Cars
               .Include(c => c.Details)
                   .ThenInclude(d => d.Transmission)
               .Include(c => c.Details)
                   .ThenInclude(d => d.DriveTrain)
               .Include(c => c.Details)
                   .ThenInclude(d => d.FuelType)
               .Include(c => c.Manufacturer)
               .Where(c => !c.IsSold)
               .Where(c => EF.Functions.Like(c.Manufacturer.Name, $"%{value}%") ||
                      EF.Functions.Like(c.Model, $"%{value}%"))
               .ToListAsync();
            if (result.Count == 0)
                throw new CarExceptions($"Car not found with name {value}");
            return result;
        }

        public async Task<Car> UpdateCarAsync(int id, Car car)
        {
            var originalCar = await GetByIdAsync(id);
            if (originalCar.IsSold)
                throw new BuyException("Car is already sold");
            car.Id = id;
            _context.Entry(originalCar).CurrentValues.SetValues(car);
            await _context.SaveChangesAsync();
            return car;
        }

        public async Task<CarDetails> UpdateCarDetails(int id, CarDetails details, Car car)
        {
            var originalDetails = await _context.CarDetails.FirstOrDefaultAsync(cd => cd.CarId == id);
            if (originalDetails == null)
                throw new BuyException("Car details can't be found");
            details.CarId = id;
            details.Id = originalDetails.Id;
            _context.Entry(originalDetails).CurrentValues.SetValues(details);
            await _context.SaveChangesAsync();
            return details;
        }

        public async Task DeleteCarAsync(int id)
        {
            var car = await GetByIdAsync(id);
            if (car.IsSold)
                throw new BuyException("Car is already sold");
            _context.Remove(car);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Car>> GetCarsPaginated(int pageNumber, int pageSize)
        {
            var cars = await _context.Cars
                .Where(c => !c.IsSold)
                .Include(c => c.Details)
                    .ThenInclude(d => d.Transmission)
                .Include(c => c.Details)
                    .ThenInclude(d => d.DriveTrain)
                .Include(c => c.Details)
                    .ThenInclude(d => d.FuelType)
                .Include(c => c.Manufacturer)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            if (!cars.Any())
                throw new CarExceptions("No cars found");

            return cars;
        }

    }
}
