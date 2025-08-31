using FinalProject.Dtos;

namespace FinalProject.Interfaces
{
    public interface ISortCarsService
    {
        Task<List<CarDto>> SortCarsByManufacturer(string name);
        Task<List<CarDto>> SortByAscending();
        Task<List<CarDto>> SortByDescending();
        Task<List<CarDto>> SortByDrivetrain(string name);
        Task<List<CarDto>> SortByFuelType(string name);
        Task<List<CarDto>> SortByTransmission(string name);
    }
}
