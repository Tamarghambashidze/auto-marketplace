using FinalProject.Extensions;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Dtos
{
    public class UserDetailsDto
    {
        [StringLength(100, ErrorMessage = "Address can't be longer than 100 characters.")]
        public string Address { get; set; } = string.Empty;
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required(ErrorMessage = "Date of birth is required.")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(UserValidations), nameof(UserValidations.ValidateDateOfBirth))]
        public DateTime DateOfBirth { get; set; } 
        public string Gender { get; set; } = string.Empty;
    }
}
