using probnygakko.DTOs;
using probnygakko.Repositories;

namespace probnygakko.Services;

public class MuzykService : IMuzykService
{
    private readonly IMuzykRepository _muzykRepository;

    public MuzykService(IMuzykRepository muzykRepository)
    {
        _muzykRepository = muzykRepository;
    }
    public async Task<ResultDTO> GetMuzykInfo(int id)
    {
        var result = await _muzykRepository.GetMuzykInfo( id);

        if (result == null)
        {
            return new ResultDTO(404, "Muzyk Not Found");
        }

        return new ResultDTO(200, "ok", result);
    }

    public async Task<ResultDTO> AddMuzyk(MuzykDTOToAdd muzykDtoToAdd)
    {
        var result = await _muzykRepository.AddMuzyk(muzykDtoToAdd);
        if (result == 0)
        {
            return new ResultDTO(404, "blad");
        }

        return new ResultDTO(200, "ok");
    }
}