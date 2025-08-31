namespace FinalProject.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public int BoughtCarId { get; set; }
        public required virtual Car BoughtCar { get; set; } 
        public int UserId { get; set; } // Foreign key
        public virtual User? User { get; set; }  // Navigation Property
        public DateTime PurchaseDate { get; set; }
        public decimal Price { get; set; }
    }
}
