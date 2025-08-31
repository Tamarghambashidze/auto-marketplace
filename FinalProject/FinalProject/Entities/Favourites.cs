using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Entities
{
  public class Favourites
  {
    [Key]
    [ForeignKey("User")]
    public int UserId { get; set; } // Foreign key
    public required User User { get; set; }
    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
  }
}
