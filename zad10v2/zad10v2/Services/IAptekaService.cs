using zad10v2.DTOs;
using zad10v2.Enums;

namespace zad10v2.Services;

public interface IAptekaService
{
    Task<ResultDTO> AddNewRecepta(PrescriptionDTO prescriptionDto);
    Task<ResultDTO> GetPatientInfo(int id);
    Task<Errors> RegisterUser(RegisterRequest model); 
    Task<TokenResult> LoginUser(LoginRequest loginRequest); 
    Task<TokenResult> RefreshUser(RefreshTokenRequest refreshToken); 
}