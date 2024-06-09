namespace zad10v2.DTOs;

public class PrescriptionDTO
{
    public PatientDTO Patient { get; set; }
    public List<MedicamentDto> Medicaments { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public string Details { get; set; }
    
}
public class PatientDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthdate { get; set; }
}


public class MedicamentDto
{
    public string Name { get; set; }
}