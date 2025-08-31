using FinalProject.Dtos;

namespace FinalProject.Interfaces
{
    public interface IBuyCarService
    {
        Task BuyCarAsync(int carId, int userId);
        Task BuyCarFromFavourites(int carId, int userId);
        Task<List<TransactionDto>> GetTransactions(int carId, IMapCarsService mapCarsService);
    }
}
