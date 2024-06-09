using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace probnygakko.Models;

public class Utwor
{
    [Key] 
    public int IdUtwor { get; set; }
    [Required][ MaxLength(30)] 
    public string  NazwaUtworu { get; set; }
    [Required] 
    public float CzasTrwania { get; set; }
    
    public int? IdAlbum { get; set; }
    
    [ForeignKey(nameof(IdAlbum))]
    public Album Albumy { get; set; }

    public ICollection<WykonawcaUtworu> Wykonawcy { get; set; }
    
    
    
    
}