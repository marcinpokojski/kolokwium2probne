using wa10.DTOs;

namespace wa10.Repositories;

public interface ITravelRepository
{
    Task<EventListDTO> GetListOfEvents(bool withDateend);
    Task<ResultDTO> DeleteEvent(int id);
}