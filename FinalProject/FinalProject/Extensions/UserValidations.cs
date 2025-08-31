using FinalProject.Entities;
using FinalProject.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Extensions
{
    public static class UserValidations
    {
        public static ValidationResult ValidateDateOfBirth(DateTime dateOfBirth, ValidationContext context)
        {
            var age = DateTime.Today.Year - dateOfBirth.Year;
            if (dateOfBirth > DateTime.Today.AddYears(-age)) age--;
            if (age < 18 || age > 100)
                return new ValidationResult("Age must be between 18 and 100 years old.");
            return ValidationResult.Success;
        }
    }
}
