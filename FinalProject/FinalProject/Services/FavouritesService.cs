using FinalProject.Dtos;
using FinalProject.Interfaces;

namespace FinalProject.Services
{
    public class FavouritesService : IFavouritesService
    {
        private readonly IFavouritesRepository _favouritesRepository;
        private readonly IMapCarsService _mapCarsService;
        public FavouritesService(IFavouritesRepository favouritesRepository, IMapCarsService mapCarsService)
        {
            _favouritesRepository = favouritesRepository;
            _mapCarsService = mapCarsService;
        }

        public async Task AddToFavouritesAsync(int carId, int userId)
        {
            await _favouritesRepository.AddCarToFavouritesAsync(userId, carId);
        }

        public async Task<List<CarDto>> GetUserFavouritesAsync(int userId)
        {
            var cars = await _favouritesRepository.GetUserFavouritesAsync(userId);
            return cars.Select(c => _mapCarsService.MapFromCarToCarDto(c)).ToList();
        }

        public async Task DeleteCarAsync(int userId, int carId)
        {
            await _favouritesRepository.DeleteCarFromFavouritesAsync(userId, carId);
        }
    }
}
