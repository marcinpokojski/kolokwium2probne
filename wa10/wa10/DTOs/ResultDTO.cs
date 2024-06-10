namespace wa10.DTOs;

public class ResultDTO
{
    public ResultDTO(int code, string message, EventListDTO list)
    {
        Code = code;
        Message = message;
        EventList = list;
    }

    public int Code { get; set; }
    public string Message { get; set; }
    public EventListDTO EventList { get; set; }
    public ResultDTO(int code, string message)
    {
        Code = code;
        Message = message;
    }
}