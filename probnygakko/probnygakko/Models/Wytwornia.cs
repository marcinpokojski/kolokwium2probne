using System.ComponentModel.DataAnnotations;

namespace probnygakko.Models;

public class Wytwornia
{
    [Key] public int IdWytwornia { get; set; }
    [MaxLength(50)] [Required] public string Nazwa { get; set; }

    public ICollection<Album> Albumy { get; set; }
}