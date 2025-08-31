using FinalProject.Entities;

namespace FinalProject.Interfaces
{
    public interface ICarDetailsRepository
    {
        Task<Manufacturer> GetByNameAsync(string name);
        Task<Transmission> GetTransmissionAsync(string name);
        Task<FuelType> GetFuelTypeAsync(string name);
        Task<DriveTrain> GetDriveTrainAsync(string name);
        Task<List<Manufacturer>> GetManufacturersAsync();
        Task<List<Transmission>> GetTransmissionsAsync();
        Task<List<FuelType>> GetFuelTypesAsync();
        Task<List<DriveTrain>> GetDriveTrainsAsync();
    }
}
