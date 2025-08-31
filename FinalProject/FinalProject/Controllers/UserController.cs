using Microsoft.AspNetCore.Mvc;
using FinalProject.Interfaces;
using FinalProject.Dtos;
using FinalProject.Exceptions;
using FinalProject.Services;

namespace FinalProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;
        private readonly IJwtService _jwtService; 

        public UserController(IUserService userService, ILogger<UserController> logger, IJwtService jwtService)
        {
            _userService = userService;
            _logger = logger;
            _jwtService = jwtService;
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] CreateUserDto userDto)
        {
            _logger.LogInformation("Attempting to register a new user with username: {Username}", userDto.Email);
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid registration data for user: {Username}", userDto.Email);
                    return BadRequest(ModelState);
                }

                await _userService.AddUserAsync(userDto);
                _logger.LogInformation("User registered successfully: {Username}", userDto.Email);
                return Ok("User added successfully");
            }
            catch (UserException ex)
            {
                _logger.LogWarning("User registration failed for {Username}. Error: {Message}", userDto.Email, ex.Message);
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("Log-in")]
        public async Task<IActionResult> LogInAsync([FromBody] UserLogIn userLogin)
        {
            _logger.LogInformation("Attempting to log in user: {Username}", userLogin.Email);
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid login attempt for user: {Username}", userLogin.Email);
                    return BadRequest(ModelState);
                }

                var user = await _userService.FindUserAsync(userLogin);
                var token = _jwtService.GenerateJwtToken(userLogin.Email);

                _logger.LogInformation("User logged in successfully: {Username}", userLogin.Email);
                return Ok(new { message = "Successful log in", user, token });
            }
            catch (UserException ex)
            {
                _logger.LogWarning("Login failed for user: {Username}. Error: {Message}", userLogin.Email, ex.Message);
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute] int id)
        {
            _logger.LogInformation("Attempting to fetch user details for userId: {UserId}", id);
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                _logger.LogInformation("Successfully fetched user details for userId: {UserId}", id);
                return Ok(user);
            }
            catch (UserException ex)
            {
                _logger.LogWarning("Failed to fetch user details for userId: {UserId}. Error: {Message}", id, ex.Message);
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateUserAsync([FromRoute] int id, [FromBody] BaseUserDto userDto)
        {
            _logger.LogInformation("Attempting to update user details for userId: {UserId}", id);
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid data for user update for userId: {UserId}", id);
                    return BadRequest(ModelState);
                }

                await _userService.UpdateUserAsync(id, userDto);
                _logger.LogInformation("User details updated successfully for userId: {UserId}", id);
                return Ok(new { message = "User successfully updated" });
            }
            catch (UserException ex)
            {
                _logger.LogWarning("Failed to update user details for userId: {UserId}. Error: {Message}", id, ex.Message);
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("Update-password")]
        public async Task<IActionResult> UpdateUserPasswordAsync([FromBody] UserPasswordUpdate user)
        {
            _logger.LogInformation("Attempting to update password for user: {Username}", user.Email);
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid password update attempt for user: {Username}", user.Email);
                    return BadRequest(ModelState);
                }

                await _userService.UpdatePasswordAsync(user);
                _logger.LogInformation("Password successfully updated for user: {Username}", user.Email);
                return Ok(new { message = "Password successfully changed" });
            }
            catch (UserException ex)
            {
                _logger.LogWarning("Failed to update password for user: {Username}. Error: {Message}", user.Email, ex.Message);
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] int id, [FromBody] UserLogIn userLogIn)
        {
            _logger.LogInformation("Attempting to delete user with userId: {UserId}", id);
            try
            {
                await _userService.DeleteUserAsync(id, userLogIn);
                _logger.LogInformation("User successfully deleted with userId: {UserId}", id);
                return Ok("User successfully deleted");
            }
            catch (UserException ex)
            {
                _logger.LogWarning("Failed to delete user with userId: {UserId}. Error: {Message}", id, ex.Message);
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
