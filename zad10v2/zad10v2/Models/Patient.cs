using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace zad10v2.Models;

public class Patient
{
    [Key] [Required] public int IdPatient { get; set; }
    [Required] [MaxLength(100)] public string FirstName { get; set; }
    [Required] [MaxLength(100)] public string LastName { get; set; }
    [Required] public DateTime Birthdate { get; set; }
    
    
   
    //nnavigation property
    public virtual ICollection<Prescription> Prescriptions { get; set; }
}