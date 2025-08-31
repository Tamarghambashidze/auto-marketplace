using FinalProject.Exceptions;
using FinalProject.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SortCarsController : Controller
    {
        private readonly ISortCarsService _sortCarsService;
        private readonly ILogger<SortCarsController> _logger;

        public SortCarsController(ISortCarsService sortCarsRepository, ILogger<SortCarsController> logger)
        {
            _sortCarsService = sortCarsRepository;
            _logger = logger;
        }

        [HttpGet("{manufacturer}")]
        public async Task<IActionResult> SortByManufacturer([FromRoute] string manufacturer)
        {
            _logger.LogInformation("Sorting cars by manufacturer: {Manufacturer}", manufacturer);
            try
            {
                var result = await _sortCarsService.SortCarsByManufacturer(manufacturer);
                _logger.LogInformation("Successfully sorted cars by manufacturer: {Manufacturer}", manufacturer);
                return Ok(result);
            }
            catch (SortExceptions ex)
            {
                _logger.LogWarning("Failed to sort cars by manufacturer: {Manufacturer}. Error: {Message}", manufacturer, ex.Message);
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("Order-by-price-ascending")]
        public async Task<IActionResult> OrderByAscending()
        {
            _logger.LogInformation("Sorting cars by price in ascending order");
            try
            {
                var result = await _sortCarsService.SortByAscending();
                _logger.LogInformation("Successfully sorted cars by price in ascending order");
                return Ok(result);
            }
            catch (CarExceptions ex)
            {
                _logger.LogWarning("Failed to sort cars by price ascending. Error: {Message}", ex.Message);
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("Order-by-price-descending")]
        public async Task<IActionResult> OrderByDescending()
        {
            _logger.LogInformation("Sorting cars by price in descending order");
            try
            {
                var result = await _sortCarsService.SortByDescending();
                _logger.LogInformation("Successfully sorted cars by price in descending order");
                return Ok(result);
            }
            catch (CarExceptions ex)
            {
                _logger.LogWarning("Failed to sort cars by price descending. Error: {Message}", ex.Message);
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("Drive-trains/{drivetrain}")]
        public async Task<IActionResult> SortByDrivetrain([FromRoute] string drivetrain)
        {
            _logger.LogInformation("Sorting cars by drivetrain: {Drivetrain}", drivetrain);
            try
            {
                var result = await _sortCarsService.SortByDrivetrain(drivetrain);
                _logger.LogInformation("Successfully sorted cars by drivetrain: {Drivetrain}", drivetrain);
                return Ok(result);
            }
            catch (SortExceptions ex)
            {
                _logger.LogWarning("Failed to sort cars by drivetrain: {Drivetrain}. Error: {Message}", drivetrain, ex.Message);
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("Fuel-types/{fuelType}")]
        public async Task<IActionResult> SortByFuelType([FromRoute] string fuelType)
        {
            _logger.LogInformation("Sorting cars by fuel type: {FuelType}", fuelType);
            try
            {
                var result = await _sortCarsService.SortByFuelType(fuelType);
                _logger.LogInformation("Successfully sorted cars by fuel type: {FuelType}", fuelType);
                return Ok(result);
            }
            catch (SortExceptions ex)
            {
                _logger.LogWarning("Failed to sort cars by fuel type: {FuelType}. Error: {Message}", fuelType, ex.Message);
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("Transmissions/{transmission}")]
        public async Task<IActionResult> SortByTransmission([FromRoute] string transmission)
        {
            _logger.LogInformation("Sorting cars by transmission: {Transmission}", transmission);
            try
            {
                var result = await _sortCarsService.SortByTransmission(transmission);
                _logger.LogInformation("Successfully sorted cars by transmission: {Transmission}", transmission);
                return Ok(result);
            }
            catch (SortExceptions ex)
            {
                _logger.LogWarning("Failed to sort cars by transmission: {Transmission}. Error: {Message}", transmission, ex.Message);
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
