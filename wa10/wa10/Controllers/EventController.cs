using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using wa10.Services;

namespace wa10.Controllers;
[ApiController]
public class EventController : ControllerBase
{
    private readonly IEventService _eventService;

    public EventController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [HttpGet]
    [Route("api/events")]
    public async Task<IActionResult> GetEventList(bool withDateend)
    {
        var result = await _eventService.GetEventList(withDateend);
        if (result.Code == 404)
        {
            return NotFound(result.Message);
        }

        return Ok(result.EventList);
    }

    [HttpDelete]
    [Route("api/events")]
    public async Task<IActionResult> Deleteevent(int id)
    {
        var result = await _eventService.DeleteEvent(id);
        if (result.Code == 404)
        {
            return NotFound(result.Message);
        }

        return Ok(result.EventList);
    }
    
    
}