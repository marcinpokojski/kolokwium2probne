using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace zad10v2.Models;

public class Prescription
{
    [Key] [Required] public int IdPrescription { get; set; }
    [Required] public DateTime Date { get; set; }
    [Required] public DateTime DueDate { get; set; }
    [Required] public int IdPatient { get; set; }
    [Required] public int IdDoctor { get; set; }

    [ForeignKey(nameof(IdPatient))] public Patient Patient { get; set; }

    [ForeignKey(nameof(IdDoctor))] 
    public Doctor Doctor { get; set; }
    
    //nav
    public virtual ICollection<Prescription_Medicament> PrescriptionMedicaments { get; set; }
}