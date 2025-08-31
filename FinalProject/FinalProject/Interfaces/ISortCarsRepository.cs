using FinalProject.Entities;

namespace FinalProject.Interfaces
{
    public interface ISortCarsRepository
    {
        Task<List<Car>> SortByManufacturer(string name);
        Task<List<Car>> SortByDriveTrain(string name);
        Task<List<Car>> SortByFuelType(string name);
        Task<List<Car>> SortByTransmission(string name);
    }
}
