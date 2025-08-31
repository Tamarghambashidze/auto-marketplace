using System.Text.Json.Serialization;

namespace FinalProject.Entities
{
    public class CarDetails
    {
        public int Id { get; set; }
        public int FuelTypeId { get; set; } // Foreign key
        public virtual required FuelType FuelType { get; set; } // Navigation property
        public int TransmissionId { get; set; }  // Foreign key
        public virtual required Transmission Transmission { get; set; } // Navigation property
        public int NumberOfDoors { get; set; }
        public bool HasSunroof { get; set; }
        public int DrivetrainId { get; set; } // Foreign key
        public virtual required DriveTrain DriveTrain { get; set; } // Navigation property
        public DateTime LastServiceDate { get; set; }
        public int CarId { get; set; } //Foreign key
        public virtual required Car Car { get; set; } // Navigation property to Car
    }
}