using FinalProject.Entities;

namespace FinalProject.Interfaces
{
    public interface IBuyCarRepository
    {
        Task BuyCarAsync(User user, Car car);
        Task BuyCarFromFavouritesAsync(int userId, int carId);
        Task<List<Transaction>> GetTransactionsAsync(int userId);
    }
}
