using System.ComponentModel.DataAnnotations;

namespace zad10v2.Models;

public class Medicament
{
    [Key] [Required] public int IdMedicament{ get; set; }
    [Required] [MaxLength(100)] public string Name { get; set; }
    [Required] [MaxLength(100)] public string Description { get; set; }
    [Required] [MaxLength(100)] public string Type { get; set; }
    [Required] [MaxLength(100)] public string Emial { get; set; }
    
    
    
    //nnavigation property do wiekszej
    public virtual ICollection<Prescription_Medicament> PrescriptionMedicaments { get; set; }
}