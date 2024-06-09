namespace koloswyklady.DTOs;

public class ResultDTO
{
   

    public int Code { get; set; }
    public string Message { get; set; }
    public ReservationDTO ReservationDto { get; set; }
    public ResultDTO(int code, string message)
    {
        Code = code;
        Message = message;
    }
    public ResultDTO(int code, string message, ReservationDTO reservationDto)
    {
        Code = code;
        Message = message;
        ReservationDto = reservationDto;
    }
   
}