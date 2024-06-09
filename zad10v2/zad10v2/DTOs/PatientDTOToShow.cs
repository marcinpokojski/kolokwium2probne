namespace zad10v2.DTOs;

public class PatientDTOToShow
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public List<PrescriptionDTOToshow> Prescriptions { get; set; }
    
    
}

public class PrescriptionDTOToshow
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public List<MedicamentDtoToShow> Medicaments { get; set; }
    public DoctorDTOToShow Doctor { get; set; }
    
}

public class MedicamentDtoToShow
{
    public int IdMedicament { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}

public class DoctorDTOToShow
{
    public int IdDoctor { get; set; }
    public string FirstName { get; set; }
}