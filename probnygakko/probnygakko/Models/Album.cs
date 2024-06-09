using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace probnygakko.Models;

public class Album
{
    [Key] 
    public int IdAlbum { get; set; }
    
    [MaxLength(30)] 
    public string NazwaAlbumu { get; set; }
    
    [Required] 
    public DateTime DataWydania { get; set; }
    
    [Required] 
    public int IdWytwornia { get; set; }
    
    [ForeignKey(nameof(IdWytwornia))] 
    public Wytwornia Wytwornie { get; set; }
    
    //navigation property
    public ICollection<Utwor> Utwory { get; set; }
}