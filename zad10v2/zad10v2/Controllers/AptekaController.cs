using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
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
    [Authorize]
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

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest model)
    {
        var result = await _aptekaService.RegisterUser(model);

        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        var result = await _aptekaService.LoginUser(loginRequest);

        if (result == null)
            return Unauthorized();

        return Ok(result);
    }
    
    [Authorize(AuthenticationSchemes = "IgnoreTokenExpirationScheme")]
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenRequest refreshToken)
    {
        var result = await _aptekaService.RefreshUser(refreshToken);

        return Ok(result);
    }
}