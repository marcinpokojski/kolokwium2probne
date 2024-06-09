using koloswyklady.DTOs;

namespace koloswyklady.Services;

public interface ISwimService
{
    Task<ResultDTO> GetClientDetails(int id);
}