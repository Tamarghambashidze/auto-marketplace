using FinalProject.Interfaces;

namespace FinalProject.Services
{
    public class CarDetailsService : ICarDetailsService
    {
        private readonly ICarDetailsRepository _carDetailsepository;
        public CarDetailsService(ICarDetailsRepository carDetailsRepository)
        {
            _carDetailsepository = carDetailsRepository;
        }

        public async Task<List<string>> GetManufacturersAsync()
        {
            var manufacturers = await _carDetailsepository.GetManufacturersAsync();
            return manufacturers.Select(m => m.Name).ToList();
        }

        public async Task<List<string>> GetFuelTypeAsync()
        {
            var fuelTypes = await _carDetailsepository.GetFuelTypesAsync();
            return fuelTypes.Select(ft => ft.Name).ToList();
        }

        public async Task<List<string>> GetDriveTrainsAsync()
        {
            var drivetrains = await _carDetailsepository.GetDriveTrainsAsync();
            return drivetrains.Select(dt => dt.Name).ToList();
        }

        public async Task<List<string>> GetTransmissionsAsync()
        {
            var transmissions = await _carDetailsepository.GetTransmissionsAsync();
            return transmissions.Select(t => t.Name).ToList();
        }
    }
}
