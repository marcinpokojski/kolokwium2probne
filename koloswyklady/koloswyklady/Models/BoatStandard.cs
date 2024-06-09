using System.ComponentModel.DataAnnotations;

namespace koloswyklady.Models;

public class BoatStandard
{
    [Key] [Required] public int IdBoatStandard { get; set; }
    [Required] [MaxLength(100)] public string Name { get; set; }
    [Required] public int Level { get; set; }
    
    //nav prop
    public ICollection<Reservation> Reservations { get; set; }
    public ICollection<SailBoat> SailBoats { get; set; }
}