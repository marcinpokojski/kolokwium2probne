namespace zad10v2.DTOs;

public class ResultDTO
{
    public ResultDTO(int code, string message, PatientDTOToShow patientDtoToShow)
    {
        Code = code;
        Message = message;
        PatientDtoToShow = patientDtoToShow;
    }

    public int Code { get; set; }
    public string Message { get; set; }
    public PatientDTOToShow PatientDtoToShow { get; set; }
    public ResultDTO(int code, string message)
    {
        Code = code;
        Message = message;
    }

   
}