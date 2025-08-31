using FinalProject.Data;
using FinalProject.Entities;
using FinalProject.Exceptions;
using FinalProject.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Repositories
{
    public class FavouritesRepository : IFavouritesRepository
    {
        private readonly ApplicationDbContext _context;
        public FavouritesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddCarToFavouritesAsync(int userId, int carId)
        {
            var car = await _context.Cars.FindAsync(carId);
            if (car == null)
                throw new FavouritesException($"Invalid car id: {carId}");
            if (car.IsSold)
                throw new BuyException("Car is already sold");

            var favourite = await _context.Favourites.Include(f => f.Cars).FirstOrDefaultAsync(f => f.UserId == userId);
            if (favourite == null)
                throw new FavouritesException($"Id: {userId} is incorrect");

            if (favourite.Cars.Any(c => c.Id == carId))
                throw new FavouritesException($"Car with id {carId} is already in the favourites");

            favourite.Cars.Add(car);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Car>> GetUserFavouritesAsync(int userId)
        {
            var favourite = await _context.Favourites
                .Include(f => f.Cars)
                    .ThenInclude(c => c.Details)
                        .ThenInclude(d => d.Transmission)
                .Include(f => f.Cars)
                    .ThenInclude(c => c.Details)
                        .ThenInclude(d => d.DriveTrain)
                .Include(f => f.Cars)
                    .ThenInclude(c => c.Details)
                        .ThenInclude(d => d.FuelType)
                .Include(f => f.Cars)
                    .ThenInclude(c => c.Manufacturer)
                .FirstOrDefaultAsync(f => f.UserId == userId);

            if (favourite == null)
                throw new FavouritesException($"Id: {userId} is incorrect");
            var cars = favourite.Cars.ToList();
            if (cars.Count == 0)
                throw new FavouritesException("No cars added");
            return cars;
        }

        public async Task DeleteCarFromFavouritesAsync(int userId, int carId)
        {
            var favourite = await _context.Favourites.Include(f => f.Cars)
                .FirstOrDefaultAsync(f => f.UserId == userId);
            if(favourite == null)
                throw new FavouritesException($"Id: {userId} is incorrect");
            var carFavourite = await _context.CarFavourites
                .FirstOrDefaultAsync(cf => cf.CarId == carId && cf.FavouriteId == userId);
            if (carFavourite == null)
                throw new FavouritesException($"Car with id {carId} is not in the favourites");
            _context.CarFavourites.Remove(carFavourite);
            await _context.SaveChangesAsync();
        }
    }
}
