using FinalProject.Dtos;
using FinalProject.Exceptions;
using FinalProject.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuyCarController : Controller
    {
        private readonly IBuyCarService _buyCarService;
        private readonly ILogger<BuyCarController> _logger;

        public BuyCarController(IBuyCarService buyCarService, ILogger<BuyCarController> logger)
        {
            _buyCarService = buyCarService;
            _logger = logger;
        }

        [HttpPost("User/{userId}/Car/{carId}")]
        public async Task<IActionResult> BuyCarAsync([FromRoute] int userId, [FromRoute] int carId)
        {
            _logger.LogInformation("User {UserId} is attempting to buy Car {CarId}", userId, carId);
            try
            {
                await _buyCarService.BuyCarAsync(carId, userId);
                _logger.LogInformation("User {UserId} successfully bought Car {CarId}", userId, carId);
                return Ok("Car bought successfully");
            }
            catch (UserException ex)
            {
                _logger.LogWarning("User {UserId} not found: {Message}", userId, ex.Message);
                return NotFound(new { message = ex.Message });
            }
            catch (CarExceptions ex)
            {
                _logger.LogWarning("Car {CarId} not found: {Message}", carId, ex.Message);
                return NotFound(new { message = ex.Message });
            }
            catch (BuyException ex)
            {
                _logger.LogWarning("Purchase failed for User {UserId} and Car {CarId}: {Message}", userId, carId, ex.Message);
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("Favourites/User/{userId}/Car/{carId}")]
        public async Task<IActionResult> BuyCarFromFavouritesAsync([FromRoute] int userId, [FromRoute] int carId)
        {
            _logger.LogInformation("User {UserId} is attempting to buy Car {CarId} from favourites", userId, carId);
            try
            {
                await _buyCarService.BuyCarFromFavourites(carId, userId);
                _logger.LogInformation("User {UserId} successfully bought Car {CarId} from favourites", userId, carId);
                return Ok("Car bought successfully");
            }
            catch (UserException ex)
            {
                _logger.LogWarning("User {UserId} not found: {Message}", userId, ex.Message);
                return NotFound(new { message = ex.Message });
            }
            catch (CarExceptions ex)
            {
                _logger.LogWarning("Car {CarId} not found: {Message}", carId, ex.Message);
                return NotFound(new { message = ex.Message });
            }
            catch (BuyException ex)
            {
                _logger.LogWarning("Purchase failed for User {UserId} and Car {CarId}: {Message}", userId, carId, ex.Message);
                return BadRequest(new { message = ex.Message });
            }
            catch (FavouritesException ex)
            {
                _logger.LogWarning("Car {CarId} is not in User {UserId}'s favourites: {Message}", carId, userId, ex.Message);
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("User/{userId}/Transactions")]
        public async Task<IActionResult> GetTransactions([FromRoute] int userId, [FromServices] IMapCarsService mapCarsService)
        {
            _logger.LogInformation("Fetching transactions for User {UserId}", userId);
            try
            {
                var transactions = await _buyCarService.GetTransactions(userId, mapCarsService);
                _logger.LogInformation("Successfully retrieved {Count} transactions for User {UserId}", transactions.Count(), userId);
                return Ok(transactions);
            }
            catch (UserException ex)
            {
                _logger.LogWarning("User {UserId} not found: {Message}", userId, ex.Message);
                return NotFound(new { message = ex.Message });
            }
            catch (BuyException ex)
            {
                _logger.LogWarning("Failed to fetch transactions for User {UserId}: {Message}", userId, ex.Message);
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
