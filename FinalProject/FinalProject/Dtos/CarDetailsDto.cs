namespace FinalProject.Dtos
{
    public class CarDetailsDto
    {
        public string FuelType { get; set; } = string.Empty;
        public string Transmission { get; set; } = string.Empty;
        public int NumberOfDoors { get; set; }
        public bool HasSunroof { get; set; }
        public string DriveTrain { get; set; } = string.Empty;
        public DateTime LastServiceDate { get; set; }
    }
}
