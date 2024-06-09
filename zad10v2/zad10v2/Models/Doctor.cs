using System.ComponentModel.DataAnnotations;

namespace zad10v2.Models;

public class Doctor
{

    [Key] [Required] public int IdDoctor { get; set; }
    [Required] [MaxLength(100)] public string FirstName { get; set; }
    [Required] [MaxLength(100)] public string LastName { get; set; }
    [Required] public string  Email { get; set; }
    
    
   
    //nnavigation property
    public virtual ICollection<Prescription> Prescriptions { get; set; }

}