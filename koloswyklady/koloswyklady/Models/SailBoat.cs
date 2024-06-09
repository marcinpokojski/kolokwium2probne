using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace koloswyklady.Models;

public class SailBoat
{
    [Key] [Required] public int IdSailboat { get; set; }
    [Required] [MaxLength(100)] public string Name { get; set; }
    [Required] public int Capacity { get; set; }
    [Required] [MaxLength(100)] public string Description { get; set; }
    [Required] public int IdBoatStandard { get; set; }
    [Required] public double Price { get; set; }
    
    //klucz obcy
    [ForeignKey(nameof(IdBoatStandard))] public BoatStandard BoatStandard { get; set; }
    
    //nav prop
    public ICollection<Sailboat_Reservation> SailboatReservations { get; set; }
    
    
    
}