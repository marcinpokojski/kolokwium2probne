using wa10.DTOs;
using wa10.Repositories;

namespace wa10.Services;

public class EventService : IEventService
{
    private readonly ITravelRepository _travelRepository;

    public EventService(ITravelRepository travelRepository)
    {
        _travelRepository = travelRepository;
    }

    public async Task<ResultDTO> GetEventList(bool withDateend)
    {
        var result =await _travelRepository.GetListOfEvents(withDateend);
        if (result == null)
        {
            return new ResultDTO(404, "blad");
        }

        return new ResultDTO(200, "ok", result);
    }

    public Task<ResultDTO> DeleteEvent(int id)
    {
        var result = _travelRepository.DeleteEvent(id);
        return result;
    }
}