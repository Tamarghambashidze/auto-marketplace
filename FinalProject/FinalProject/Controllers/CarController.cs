using FinalProject.Exceptions;
using FinalProject.Interfaces;
using FinalProject.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : Controller
    {
        private readonly ICarService _carService;
        private readonly ILogger<CarController> _logger;

        public CarController(ICarService carService, ILogger<CarController> logger)
        {
            _carService = carService;
            _logger = logger;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddCarAsync([FromBody] BaseCarDto carDto)
        {
            _logger.LogInformation("Received request to add a car. Data: {@CarDto}", carDto);
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state while adding a car.");
                    return BadRequest(ModelState);
                }

                await _carService.AddCarAsync(carDto);
                _logger.LogInformation("Car added successfully");
                return Ok(new { message = "Car added successfully" });
            }
            catch (CarExceptions ex)
            {
                _logger.LogWarning("Car addition failed: {Message}", ex.Message);
                return NotFound(new { message = ex.Message });
            }
        }
        [HttpGet("Get-all")]
        public async Task<IActionResult> GetAllCarsAsync()
        {
            _logger.LogInformation("Fetching all cars...");
            try
            {
                var cars = await _carService.GetAllCars();
                _logger.LogInformation("Successfully retrieved {Count} cars.", cars.Count());
                return Ok(cars);
            }
            catch (CarExceptions ex)
            {
                _logger.LogWarning("Fetching all cars failed: {Message}", ex.Message);
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("Get-paginated")]
        public async Task<IActionResult> GetPaginatedCars([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            _logger.LogInformation("Fetching paginated cars (Page: {PageNumber}, Size: {PageSize})", pageNumber, pageSize);
            try
            {
                var cars = await _carService.GetCarsPaginated(pageNumber, pageSize);
                _logger.LogInformation("Retrieved {Count} cars on page {PageNumber}.", cars.Count(), pageNumber);
                return Ok(cars);
            }
            catch (CarExceptions ex)
            {
                _logger.LogWarning("Paginated fetch failed: {Message}", ex.Message);
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            _logger.LogInformation("Fetching car with ID: {CarId}", id);
            try
            {
                var car = await _carService.GetCarById(id);
                _logger.LogInformation("Car with ID {CarId} found.", id);
                return Ok(car);
            }
            catch (CarExceptions ex)
            {
                _logger.LogWarning("Car with ID {CarId} not found: {Message}", id, ex.Message);
                return NotFound(new { message = ex.Message });
            }
            catch (BuyException ex)
            {
                _logger.LogWarning("Invalid request for car ID {CarId}: {Message}", id, ex.Message);
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("Search")]
        public async Task<IActionResult> SearchCar([FromQuery] string value)
        {
            _logger.LogInformation("Searching for cars with value: {SearchValue}", value);
            try
            {
                var cars = await _carService.SearchCar(value);
                _logger.LogInformation("Search found {Count} cars for value '{SearchValue}'.", cars.Count(), value);
                return Ok(cars);
            }
            catch (CarExceptions ex)
            {
                _logger.LogWarning("Search failed: {Message}", ex.Message);
                return NotFound(new { message = ex.Message });
            }
            catch (BuyException ex)
            {
                _logger.LogWarning("Invalid search request: {Message}", ex.Message);
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateCarAsync([FromRoute] int id, [FromBody] BaseCarDto car)
        {
            _logger.LogInformation("Updating car with ID: {CarId}", id);
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state while updating car ID {CarId}.", id);
                    return BadRequest(ModelState);
                }

                await _carService.UpdateCarAsync(id, car);
                _logger.LogInformation("Car with ID {CarId} updated successfully.", id);
                return Ok(new { message = "Car updated successfully" });
            }
            catch (BuyException ex)
            {
                _logger.LogWarning("Car update failed: {Message}", ex.Message);
                return BadRequest(new { message = ex.Message });
            }
            catch (CarExceptions ex)
            {
                _logger.LogWarning("Car with ID {CarId} not found for update: {Message}", id, ex.Message);
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while updating car with ID {CarId}.", id);
                return StatusCode(500, new { message = "An unexpected error occurred", error = ex.Message });
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteCarAsync([FromRoute] int id)
        {
            _logger.LogInformation("Deleting car with ID: {CarId}", id);
            try
            {
                await _carService.DeleteCarAsync(id);
                _logger.LogInformation("Car with ID {CarId} deleted successfully.", id);
                return Ok(new { message = "Car deleted successfully" });
            }
            catch (CarExceptions ex)
            {
                _logger.LogWarning("Car deletion failed: {Message}", ex.Message);
                return NotFound(new { message = ex.Message });
            }
            catch (BuyException ex)
            {
                _logger.LogWarning("Invalid request to delete car: {Message}", ex.Message);
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}


