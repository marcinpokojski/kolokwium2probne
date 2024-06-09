using probnygakko.Models;

namespace probnygakko.DTOs;

public class MuzykDTO
{
    public int IdMuzyk { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Pseudonim { get; set; }
    public List<UtworDTO> Utwory { get; set; }
}
public class UtworDTO
{
    public int IdUtwor { get; set; }
    public string NazwaUtworu { get; set; }
    public float CzasTrwania { get; set; }
}