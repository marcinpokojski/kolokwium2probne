using System.ComponentModel.DataAnnotations;

namespace wa10.Models;

public class Event
{
    [Key] [Required] public int IdEvent { get; set; }
    [Required][MaxLength(60)] public string Name { get; set; }
    [Required] public DateTime DateFrom { get; set; }
    public DateTime? DateTo { get; set; }

    public virtual ICollection<EventOrganiser> EventOrganisers { get; set; }
}