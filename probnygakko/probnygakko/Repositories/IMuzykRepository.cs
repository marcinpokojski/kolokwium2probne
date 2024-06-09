using probnygakko.DTOs;

namespace probnygakko.Repositories;

public interface IMuzykRepository
{
    Task<MuzykDTO> GetMuzykInfo(int id);
    //Task<bool> DoesMuzykExist(int id);
    Task<int> AddMuzyk(MuzykDTOToAdd muzykDtoToAdd);
}