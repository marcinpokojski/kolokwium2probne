using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace probnygakko.Models;

public class WykonawcaUtworu
{
    [Key,Column(Order = 0)]
    public int IdMuzyk { get; set; }
    [Key,Column(Order = 1)] 
    public int IdUtwor { get; set; }
    
    [ForeignKey(nameof(IdMuzyk))] 
    public Muzyk Muzycy { get; set; }
    
    [ForeignKey(nameof(IdUtwor))]
    public Utwor  Utwory { get; set; }
}