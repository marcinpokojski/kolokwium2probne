using zad10v2.DTOs;
using zad10v2.Enums;

namespace zad10v2.Repositories;

public interface IAptekaRepsitory
{
    Task<Errors> AddNewRecepta(PrescriptionDTO prescriptionDto);
    Task<bool> DoesPatientExixt(PrescriptionDTO prescriptionDto);
    Task<PatientDTOToShow> GetPatientInfo(int id);
    
    Task<Errors> AddUser(RegisterRequest model);
    Task<TokenResult> LoginUser(LoginRequest loginRequest); 
    Task<TokenResult> RefreshUser(RefreshTokenRequest refreshToken); 
}
