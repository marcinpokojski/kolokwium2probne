using Microsoft.AspNetCore.Mvc;
using probnygakko.DTOs;
using probnygakko.Services;

namespace probnygakko.Controllers;
[ApiController]
public class MuzykController : ControllerBase
{
    private readonly IMuzykService _muzykService;

    public MuzykController(IMuzykService muzykService)
    {
        _muzykService = muzykService;
    }
    [HttpGet]
    [Route("api/muzyk/{id}")]
    public async Task<IActionResult> GetMuzykInfo(int id)
    {
        var result = await _muzykService.GetMuzykInfo(id);

        if (result.Code == 404)
        {
            return NotFound(result.Message);
        }

        return Ok(result.MuzykDto);
    }

    [HttpPost]
    [Route("api/muzyk")]
    public async Task<IActionResult> AddMuzyk(MuzykDTOToAdd muzykDtoToAdd)
    {
        var result = await _muzykService.AddMuzyk(muzykDtoToAdd);

        if (result.Code == 404)
        {
            return NotFound(result.Message);
        }

        return Ok(result.Message);
    }

}