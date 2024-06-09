
using Microsoft.AspNetCore.Http.HttpResults;
using zad10v2.DTOs;
using zad10v2.Enums;
using zad10v2.Repositories;

namespace zad10v2.Services;

public class AptekaService : IAptekaService
{
    private readonly IAptekaRepsitory _aptekaRepsitory;

    public AptekaService(IAptekaRepsitory aptekaRepsitory)
    {
        _aptekaRepsitory = aptekaRepsitory;
    }

    public async Task<ResultDTO> AddNewRecepta(PrescriptionDTO prescriptionDto)
    {
        var result = await _aptekaRepsitory.AddNewRecepta(prescriptionDto);
        if (result == Errors.Ok)
        {
            return new ResultDTO(200, "okok");
        }
        else
        {
            return new ResultDTO(404, result.ToString());
        }
        
    }

    public async Task<ResultDTO> GetPatientInfo(int id)
    {
        var result = await _aptekaRepsitory.GetPatientInfo(id);
        if (result == null)
        {
            return new ResultDTO(404, "error");
        }

        return new ResultDTO(200, "ok", result);
    }
}