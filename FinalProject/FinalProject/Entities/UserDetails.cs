using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Entities
{
  public class UserDetails
  {
    [Key]
    [ForeignKey("User")]
    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; } = string.Empty;
    public int UserId { get; set; } // Foreign Key
    public virtual required User User { get; set; } // Navigation Property
  }

}
