using wa10.DTOs;

namespace wa10.Services;

public interface IEventService
{
    Task<ResultDTO> GetEventList(bool withDateend);
    Task<ResultDTO> DeleteEvent(int id);
}