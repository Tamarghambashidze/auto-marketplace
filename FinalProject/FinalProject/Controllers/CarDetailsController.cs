using FinalProject.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarDetailsController : Controller
    {
        private readonly ICarDetailsService _carDetailsService;
        private readonly ILogger<CarDetailsController> _logger;

        public CarDetailsController(ICarDetailsService carDetailsService, ILogger<CarDetailsController> logger)
        {
            _carDetailsService = carDetailsService;
            _logger = logger;
        }

        [HttpGet("Manufacturers")]
        public async Task<IActionResult> GetManufacturersAsync()
        {
            _logger.LogInformation("Fetching list of manufacturers...");
            var manufacturers = await _carDetailsService.GetManufacturersAsync();
            _logger.LogInformation("Successfully retrieved {Count} manufacturers.", manufacturers.Count());
            return Ok(manufacturers);
        }

        [HttpGet("Transmissions")]
        public async Task<IActionResult> GetTransmissionsAsync()
        {
            _logger.LogInformation("Fetching list of transmissions...");
            var transmissions = await _carDetailsService.GetTransmissionsAsync();
            _logger.LogInformation("Successfully retrieved {Count} transmissions.", transmissions.Count());
            return Ok(transmissions);
        }

        [HttpGet("Fuel-types")]
        public async Task<IActionResult> GetFuelTypesAsync()
        {
            var fuelTypes = await _carDetailsService.GetFuelTypeAsync();
            _logger.LogInformation("Successfully retrieved {Count} fuel types.", fuelTypes.Count());
            return Ok(fuelTypes);
        }

        [HttpGet("Drive-trains")]
        public async Task<IActionResult> GetDriveTrainsAsync()
        {
            _logger.LogInformation("Fetching list of drive trains...");
            var driveTrains = await _carDetailsService.GetDriveTrainsAsync();
            _logger.LogInformation("Successfully retrieved {Count} drive trains.", driveTrains.Count());
            return Ok(driveTrains);
        }
    }
}
