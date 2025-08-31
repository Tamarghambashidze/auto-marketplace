using FinalProject.Exceptions;
using FinalProject.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavouritesController : Controller
    {
        private readonly IFavouritesService _favouritesService;
        private readonly ILogger<FavouritesController> _logger;

        public FavouritesController(IFavouritesService favouritesService, ILogger<FavouritesController> logger)
        {
            _favouritesService = favouritesService;
            _logger = logger;
        }

        [HttpPost("{carId}/User/{userId}")]
        public async Task<IActionResult> AddCarAsync([FromRoute] int carId, [FromRoute] int userId)
        {
            _logger.LogInformation("Adding car with ID {CarId} to favourites for user with ID {UserId}", carId, userId);
            try
            {
                await _favouritesService.AddToFavouritesAsync(carId, userId);
                _logger.LogInformation("Car with ID {CarId} successfully added to favourites for user with ID {UserId}", carId, userId);
                return Ok(new { message = "Car added successfully" });
            }
            catch (FavouritesException ex)
            {
                _logger.LogWarning("Failed to add car with ID {CarId} to favourites for user with ID {UserId}: {Message}", carId, userId, ex.Message);
                return NotFound(new { message = ex.Message });
            }
            catch (BuyException ex)
            {
                _logger.LogWarning("Failed to add car with ID {CarId} to favourites due to buying exception: {Message}", carId, ex.Message);
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{userId}/Favourites")]
        public async Task<IActionResult> GetUserFavouritesAsync([FromRoute] int userId)
        {
            _logger.LogInformation("Fetching favourites for user with ID {UserId}", userId);
            try
            {
                var cars = await _favouritesService.GetUserFavouritesAsync(userId);
                _logger.LogInformation("Successfully retrieved {Count} favourites for user with ID {UserId}", cars.Count(), userId);
                return Ok(cars);
            }
            catch (FavouritesException ex)
            {
                _logger.LogWarning("Failed to retrieve favourites for user with ID {UserId}: {Message}", userId, ex.Message);
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("User/{userId}/Favourites/Car/{carId}")]
        public async Task<IActionResult> DeleteCarAsync([FromRoute] int userId, [FromRoute] int carId)
        {
            _logger.LogInformation("Deleting car with ID {CarId} from favourites for user with ID {UserId}", carId, userId);
            try
            {
                await _favouritesService.DeleteCarAsync(userId, carId);
                _logger.LogInformation("Car with ID {CarId} successfully deleted from favourites for user with ID {UserId}", carId, userId);
                return Ok(new { message = "Car deleted successfully", carId });
            }
            catch (FavouritesException ex)
            {
                _logger.LogWarning("Failed to delete car with ID {CarId} from favourites for user with ID {UserId}: {Message}", carId, userId, ex.Message);
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
