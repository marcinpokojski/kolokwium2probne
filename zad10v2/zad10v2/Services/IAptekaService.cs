using zad10v2.DTOs;

namespace zad10v2.Services;

public interface IAptekaService
{
    Task<ResultDTO> AddNewRecepta(PrescriptionDTO prescriptionDto);
    Task<ResultDTO> GetPatientInfo(int id);
}