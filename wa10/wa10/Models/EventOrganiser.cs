using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wa10.Models;

public class EventOrganiser
{
    [Key,Column(Order = 0)] [Required] public int IdEvent { get; set; }
    [Key,Column(Order = 1)] [Required] public int IdOrganiser { get; set; }
    [Required] public bool MainOrganiser { get; set; }
    
    [ForeignKey(nameof(IdEvent))] public Event Event { get; set; }
    [ForeignKey((nameof(IdOrganiser)))] public Organiser Organiser { get; set; }

}