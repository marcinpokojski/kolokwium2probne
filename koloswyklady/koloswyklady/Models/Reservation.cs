using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace koloswyklady.Models;

public class Reservation
{
    [Key] [Required] public int IdReservation { get; set; }
    [Required] public int IdClient { get; set; }
    [Required] public DateTime DateFrom { get; set; }
    [Required] public DateTime DateTo { get; set; }
    [Required] public int IdBoatStandard { get; set; }
    [Required] public int Capacity { get; set; }
    [Required] public int NumberOfBoats { get; set; }
    [Required] public int Fullfilled { get; set; }
    public double?  Price { get; set; }
    [MaxLength(100)] public string? CancelReason { get; set; }
    
    [ForeignKey(nameof(IdClient))] public Client Client { get; set; }
    [ForeignKey(nameof(IdBoatStandard))] public BoatStandard BoatStandard { get; set; }
    
    //nav prop
    public ICollection<Sailboat_Reservation> SailboatReservations { get; set; }
}