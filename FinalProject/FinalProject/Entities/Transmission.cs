namespace FinalProject.Entities
{
  public class Transmission
  {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public virtual ICollection<CarDetails> Cars { get; set; } = new List<CarDetails>();
  }
}
