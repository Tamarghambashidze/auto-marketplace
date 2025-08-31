using AutoMapper;
using FinalProject.Dtos;
using FinalProject.Interfaces;
namespace FinalProject.Services
{
    public class BuyCarService : IBuyCarService
    {
        private readonly IBuyCarRepository _buyCarRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;
        public BuyCarService(IBuyCarRepository buyCarRepository, IUserRepository userRepository, 
            ICarRepository carRepository, IMapper mapper)
        {
            _buyCarRepository = buyCarRepository;
            _userRepository = userRepository;
            _carRepository = carRepository;
            _mapper = mapper;
        }
        
        public async Task BuyCarAsync(int carId, int userId)
        {
            var car = await _carRepository.GetByIdAsync(carId);
            var user = await _userRepository.GetUserById(userId);
            await _buyCarRepository.BuyCarAsync(user, car);
        }

        public async Task BuyCarFromFavourites(int carId, int userId)
        {
            await _buyCarRepository.BuyCarFromFavouritesAsync(userId, carId);
            await BuyCarAsync(carId, userId);
        }

        public async Task<List<TransactionDto>> GetTransactions(int carId, IMapCarsService mapCarsService)
        {
            var transactions = await _buyCarRepository.GetTransactionsAsync(carId);
            var transactionDtos = transactions.Select(t =>
            {
                var transactionDto = _mapper.Map<TransactionDto>(t);
                transactionDto.BoughtCar = mapCarsService.MapFromCarToCarDto(t.BoughtCar);
                return transactionDto;
            }).ToList();
            return transactionDtos;
        }
    }
}
