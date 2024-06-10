namespace wa10.DTOs;

public class EventDTOtoshow
{
    public int IdEvent { get; set; }
    public string Name { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public List<OrganiserDTO> MainOrganisers { get; set; }
    public List<OrganiserDTO> SubOrganisers { get; set; }
}

public class OrganiserDTO
{
    public int IdOrganiser { get; set; }
    public string Name { get; set; }
}