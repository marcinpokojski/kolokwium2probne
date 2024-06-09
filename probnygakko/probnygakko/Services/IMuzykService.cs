using probnygakko.DTOs;

namespace probnygakko.Services;

public interface IMuzykService
{
    Task<ResultDTO> GetMuzykInfo(int id);
    Task<ResultDTO> AddMuzyk(MuzykDTOToAdd muzykDtoToAdd);
}