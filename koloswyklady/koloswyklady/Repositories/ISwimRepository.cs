using koloswyklady.DTOs;

namespace koloswyklady.Repositories;

public interface ISwimRepository
{
    Task<ReservationDTO> GetClientDetails(int id);
}