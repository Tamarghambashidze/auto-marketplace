using System.ComponentModel.DataAnnotations;

namespace FinalProject.Dtos
{
    public class BaseUserDto
    {
        [Url(ErrorMessage = "Invalid URL format")]
        public string ProfileImgUrl { get; set; } = "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_1280.png";

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name can't be longer than 50")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name can't be longer than 50")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; } = string.Empty;
        public UserDetailsDto UserDetails { get; set; } = new UserDetailsDto();
    }

    public class CreateUserDto : BaseUserDto
    {
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password should be at minimum 8 characters")]
        public string PasswordHash { get; set; } = string.Empty;
    }
    public class UserDto : BaseUserDto
    {
        public int Id { get; set; }
    }
}
