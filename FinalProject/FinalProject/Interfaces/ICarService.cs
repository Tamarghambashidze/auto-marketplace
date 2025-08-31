using FinalProject.Entities;
using FinalProject.Dtos;

namespace FinalProject.Interfaces
{
  public interface ICarService
  {
        Task AddCarAsync(BaseCarDto carDto);
        Task<List<CarDto>> GetAllCars();
        Task<CarDto> GetCarById(int id);
        Task<List<CarDto>> SearchCar(string value);
        Task<CarDto> UpdateCarAsync(int id, BaseCarDto carDto);
        Task DeleteCarAsync(int id);
        Task<List<CarDto>> GetCarsPaginated(int pageNumber, int pageSize);
  }
}
