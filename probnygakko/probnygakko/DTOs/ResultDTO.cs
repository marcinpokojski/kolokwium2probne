namespace probnygakko.DTOs;

public class ResultDTO
{
    
    public int Code { get; set; }
    public string Message { get; set; }
    public MuzykDTO MuzykDto;
    public ResultDTO(int code, string message)
    {
        Code = code;
        Message = message;
    }

    public ResultDTO( int code, string message,MuzykDTO muzykDto)
    {
        MuzykDto = muzykDto;
        Code = code;
        Message = message;
    }
}