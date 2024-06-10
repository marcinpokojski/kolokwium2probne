using System.ComponentModel.DataAnnotations;

namespace wa10.Models;

public class Organiser
{
    [Key][Required] public int IdOrganiser { get; set; }
    [Required][MaxLength(50)] public string Name { get; set; }

    public virtual ICollection<EventOrganiser> EventOrganisers { get; set; }
}