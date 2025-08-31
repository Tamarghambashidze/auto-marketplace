using FinalProject.Data;
using FinalProject.Entities;
using FinalProject.Exceptions;
using FinalProject.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Repositories
{
    public class CarDetailsRepository : ICarDetailsRepository
    {
        private readonly ApplicationDbContext _context;
        public CarDetailsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Manufacturer> GetByNameAsync(string name)
        {
            var manufacturer = await _context.Manufacturers.FirstOrDefaultAsync(m => m.Name == name);
            if (manufacturer == null)
                throw new CarExceptions($"Manufacturer with name: {name} doesn't exist");
            return manufacturer;
        }
        public async Task<Transmission> GetTransmissionAsync(string name)
        {
            var transmission = await _context.Transmissions.FirstOrDefaultAsync(t => t.Name == name);
            if(transmission == null)
                throw new CarExceptions($"Transmission with name: {name} doesn't exist");
            return transmission;
        }
        public async Task<FuelType> GetFuelTypeAsync(string name)
        {
            var fuelType = await _context.FuelTypes.FirstOrDefaultAsync(ft => ft.Name == name);
            if (fuelType == null)
                throw new CarExceptions($"Fuel type with name: {name} doesn't exist");
            return fuelType;
        }
        public async Task<DriveTrain> GetDriveTrainAsync(string name)
        {
            var drivetrain = await _context.DriveTrains.FirstOrDefaultAsync(dt => dt.Name == name);
            if (drivetrain == null)
                throw new CarExceptions($"Drive train with name: {name} doesn't exist");
            return drivetrain;
        }

        public async Task<List<Manufacturer>> GetManufacturersAsync()
        {
            return await _context.Manufacturers.ToListAsync();
        }

        public async Task<List<Transmission>> GetTransmissionsAsync()
        {
            return await _context.Transmissions.ToListAsync();
        }

        public async Task<List<FuelType>> GetFuelTypesAsync()
        {
            return await _context.FuelTypes.ToListAsync();
        }

        public async Task<List<DriveTrain>> GetDriveTrainsAsync()
        {
            return await _context.DriveTrains.ToListAsync();
        }
    }
}
