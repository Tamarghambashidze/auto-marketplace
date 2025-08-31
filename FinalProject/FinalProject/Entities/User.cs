namespace FinalProject.Entities
{
  public class User
  {
    public int Id { get; set; }
    public string ProfileImgUrl { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public virtual required UserDetails UserDetails { get; set; } // Navigation Property
    public virtual Favourites? Favourites { get; set; } // Navigation Propery
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
  }

}
