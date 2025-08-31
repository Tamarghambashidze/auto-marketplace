namespace FinalProject.Interfaces
{
    public interface ICarDetailsService
    {
        Task<List<string>> GetManufacturersAsync();
        Task<List<string>> GetTransmissionsAsync();
        Task<List<string>> GetFuelTypeAsync();
        Task<List<string>> GetDriveTrainsAsync();
    }
}