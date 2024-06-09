using System.ComponentModel.DataAnnotations;

namespace probnygakko.Models;

public class Muzyk
{
    [Key] public int IdMuzyk { get; set; }
    [Required] [MaxLength(30)] public string FirstName { get; set; }
    [Required] [MaxLength(40)] public string LastName { get; set; }
    [MaxLength(50)] public string? Pseudonim { get; set; }

    public virtual  ICollection<WykonawcaUtworu> Wykonawcy { get; set; }
    
}