namespace probnygakko.DTOs;

public class MuzykDTOToAdd
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Pseudonim { get; set; }
    public List<UtworToAdd> Utwory { get; set; }
}

public class UtworToAdd
{
    public string NazwaUtworu { get; set; }
    public float CzasTrwania { get; set; }
    public int? IdAlbum { get; set; }
}