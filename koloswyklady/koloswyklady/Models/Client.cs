using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace koloswyklady.Models;

public class Client
{
    [Key] [Required] public int IdClient { get; set; }
    [Required] [MaxLength(100)] public string Name { get; set; }
    [Required] [MaxLength(100)] public string LastName { get; set; }
    [Required] public DateTime Birthday { get; set; }
    [Required] [MaxLength(100)] public string Pesel { get; set; }
    [Required] [MaxLength(100)] public string Emial { get; set; }
    [Required] public int IdClientCategory { get; set; }
    
    [ForeignKey(nameof(IdClientCategory))]
    public ClientCateogry ClientCateogry { get; set; }
    
    //nnavigation property
    public virtual ICollection<Reservation> Reservations { get; set; }
    
    
}