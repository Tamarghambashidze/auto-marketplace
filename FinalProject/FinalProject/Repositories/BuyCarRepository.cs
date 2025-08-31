using FinalProject.Data;
using FinalProject.Entities;
using FinalProject.Exceptions;
using FinalProject.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Repositories
{
    public class BuyCarRepository : IBuyCarRepository
    {
        private readonly ApplicationDbContext _context;
        public BuyCarRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task BuyCarAsync(User user, Car car)
        {
            if(car.IsSold)
                throw new BuyException("Car is already sold");
            var favourite = await _context.CarFavourites.FirstOrDefaultAsync(cf => cf.Car == car && cf.FavouriteId == user.Id);
            if (favourite != null)
                _context.CarFavourites.Remove(favourite);
            var transaction = new Transaction() { BoughtCar = car, User = user, Price = car.Price, PurchaseDate = DateTime.Now };
            await _context.Transactions.AddAsync(transaction);
            car.IsSold = true;
            await _context.SaveChangesAsync();
        }

        public async Task BuyCarFromFavouritesAsync(int userId, int carId)
        {
            var favourite = await _context.CarFavourites
                .FirstOrDefaultAsync(cf => cf.CarId == carId && cf.FavouriteId == userId);
            if (favourite == null)
                throw new FavouritesException($"car with id: {carId} is not added in favourites");
            _context.CarFavourites.Remove(favourite);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Transaction>> GetTransactionsAsync(int userId)
        {
            var user = await _context.Users
                .Include(u => u.Transactions)
                    .ThenInclude(t => t.BoughtCar)
                       .ThenInclude(c => c.Manufacturer)
                 .Include(u => u.Transactions)
                     .ThenInclude(t => t.BoughtCar)
                        .ThenInclude(c => c.Details)
                           .ThenInclude(d => d.FuelType)
                 .Include(u => u.Transactions)
                     .ThenInclude(t => t.BoughtCar)
                        .ThenInclude(c => c.Details)
                           .ThenInclude(d => d.Transmission)
                 .Include(u => u.Transactions)
                     .ThenInclude(t => t.BoughtCar)
                        .ThenInclude(c => c.Details)
                           .ThenInclude(d => d.DriveTrain)
                .FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                throw new UserException("user not found");
            if (user.Transactions.Count() == 0)
                throw new BuyException("No cars bought");
            return user.Transactions.ToList();
        }
    }
}
