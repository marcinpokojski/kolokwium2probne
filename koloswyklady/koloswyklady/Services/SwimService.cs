using koloswyklady.DTOs;
using koloswyklady.Repositories;

namespace koloswyklady.Services;

public class SwimService : ISwimService
{
    private readonly ISwimRepository _swimRepository;

    public SwimService(ISwimRepository swimRepository)
    {
        _swimRepository = swimRepository;
    }

    public async  Task<ResultDTO> GetClientDetails(int id)
    {
        var result = await _swimRepository.GetClientDetails(id);
        if (result == null)
        {
            return new ResultDTO(404, "blad");
;        }

        return new ResultDTO(200, "ok", result);
    }
}