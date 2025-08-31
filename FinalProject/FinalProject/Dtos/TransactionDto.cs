namespace FinalProject.Dtos
{
    public class TransactionDto
    {
        public required CarDto BoughtCar { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal Price { get; set; }

    }
}
