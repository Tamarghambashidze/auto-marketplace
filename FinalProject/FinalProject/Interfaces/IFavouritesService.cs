using FinalProject.Dtos;

namespace FinalProject.Interfaces
{
    public interface IFavouritesService
    {
        Task AddToFavouritesAsync(int carId, int userId);
        Task<List<CarDto>> GetUserFavouritesAsync(int userId);
        Task DeleteCarAsync(int userId, int carId);
    }
}
