namespace FinalProject.Entities
{
  public class Manufacturer
  {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
  }
}
