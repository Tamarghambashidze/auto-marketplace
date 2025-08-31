using System.ComponentModel.DataAnnotations;

namespace FinalProject.Dtos
{
    public class BaseCarDto
    {
        [Required]
        [Url(ErrorMessage = "Invalid URL format")]
        public string ImgUrl1 { get; set; } = string.Empty;
        [Required]
        [Url(ErrorMessage = "Invalid URL format")]
        public string ImgUrl2 { get; set; } = string.Empty;
        [Required]
        public string Manufacturer { get; set; } = string.Empty;
        [Required]
        public string Model { get; set; } = string.Empty;

        [Range(1900, 2025, ErrorMessage = "Year must be between 1900 and 2025")]
        public int Year { get; set; }
        [Required]
        public string Color { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value")]
        public decimal Price { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Mileage must be a positive value")]
        public double Mileage { get; set; }
        public CarDetailsDto Details { get; set; } = new CarDetailsDto();
    }

    public class CarDto : BaseCarDto
    {
        public int Id { get; set; }
    }
}
