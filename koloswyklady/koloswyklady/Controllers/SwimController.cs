using koloswyklady.Services;
using Microsoft.AspNetCore.Mvc;

namespace koloswyklady.Controllers;
[ApiController]
public class SwimController : ControllerBase
{
    private readonly ISwimService _swimService;

    public SwimController(ISwimService swimService)
    {
        _swimService = swimService;
    }

    [HttpGet]
    [Route("api/client/{id}")]
    public async Task<IActionResult> GetClientInfo(int id)
    {
        var result = await _swimService.GetClientDetails(id);
        if (result.Code == 404)
        {
            return NotFound(result.Message);
        }

        return Ok(result.ReservationDto);
    }
}