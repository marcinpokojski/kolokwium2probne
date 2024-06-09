using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace zad10v2.Models;

public class Prescription_Medicament
{
    [Key,Column(Order = 0)] [Required] public int IdMedicament { get; set; }
    [Key,Column(Order = 1)] [Required] public int IdPrescription { get; set; }
    public int? Dose { get; set; }
    [Required] [MaxLength(100)] public string Details { get; set; }
    
  
    [ForeignKey(nameof(IdMedicament))]
    public Medicament Medicament { get; set; }
    
    [ForeignKey((nameof(IdPrescription)))]
    public Prescription Prescription { get; set; }
    
   
}