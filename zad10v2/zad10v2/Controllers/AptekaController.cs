using Microsoft.AspNetCore.Mvc;
using zad10v2.DTOs;
using zad10v2.Services;

namespace zad10v2.Controllers;
[ApiController]
public class AptekaController : ControllerBase
{
    private readonly IAptekaService _aptekaService;

    public AptekaController(IAptekaService aptekaService)
    {
        _aptekaService = aptekaService;
    }

    [HttpPost]
    [Route("api/prescription")]
    public async Task<IActionResult> AddPrescription(PrescriptionDTO prescriptionDto)
    {
        var result = await _aptekaService.AddNewRecepta(prescriptionDto);
        if (result.Code == 404)
        {
            return NotFound(result.Message);
        }

        return Ok(result.Message);
    }

    [HttpGet]
    [Route("api/patients/{id}")]
    public async Task<IActionResult> GetInfo(int id)
    {
        var result = await _aptekaService.GetPatientInfo(id);
        if (result.Code == 404)
        {
            return NotFound(result.Message);
        }

        return Ok(result.PatientDtoToShow);
    }
}