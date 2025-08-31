namespace FinalProject.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string ImgUrl1 { get; set; } = string.Empty;
        public string ImgUrl2 { get; set; } = string.Empty;
        public int ManufacturerId { get; set; } // Foreign key
        public virtual required Manufacturer Manufacturer { get; set; }  // navigation property
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Color { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public double Mileage { get; set; }
        public virtual required CarDetails Details { get; set; } // Navigation property
        public virtual Transaction? Transaction { get; set; }
        public bool IsSold { get; set; } = false;
        public ICollection<Favourites> Favourites { get; set; } = new List<Favourites>();
    }
}
