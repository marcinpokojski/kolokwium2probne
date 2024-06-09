using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace koloswyklady.Models;

public class Sailboat_Reservation
{
    [Key,Column(Order = 0)][Required] public int IdSailboat { get; set; }
    [Key,Column(Order = 1)][Required] public int IdReservation { get; set; }
    
    [ForeignKey(nameof(IdSailboat))] public SailBoat SailBoat { get; set; }
    [ForeignKey(nameof(IdReservation))] public Reservation Reservation { get; set; }
}