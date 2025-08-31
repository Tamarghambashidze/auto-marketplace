using FinalProject.Entities;

namespace FinalProject.Interfaces
{
    public interface IFavouritesRepository
    {
        Task AddCarToFavouritesAsync(int userId, int carId);
        Task<List<Car>> GetUserFavouritesAsync(int userId);
        Task DeleteCarFromFavouritesAsync(int userId, int carId);
    }
}
