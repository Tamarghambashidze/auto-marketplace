using FinalProject.Entities;

namespace FinalProject.Interfaces
{
  public interface ICarRepository
  {
        Task<Car> AddCarAsync(Car car);
        Task<CarDetails> AddCarDetailsAsync(CarDetails carDetails);
        Task<List<Car>> GetAllCars();
        Task<Car> GetByIdAsync(int id);
        Task<List<Car>> SearchCar(string value);
        Task<Car> UpdateCarAsync(int id, Car car);
        Task<CarDetails> UpdateCarDetails(int id, CarDetails details, Car car);
        Task DeleteCarAsync(int id);
        Task<List<Car>> GetCarsPaginated(int pageNumber, int pageSize);
  }
}
