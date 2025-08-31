using System.ComponentModel.DataAnnotations;

namespace FinalProject.Dtos
{
    public class UserLogIn
    {
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
    public class UserPasswordUpdate
    {
        public string Email { get; set; } = string.Empty;
        public string OldPassword { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password should be at minimum 8 characters")]
        public string NewPassword { get; set; } = string.Empty;
    }
}
