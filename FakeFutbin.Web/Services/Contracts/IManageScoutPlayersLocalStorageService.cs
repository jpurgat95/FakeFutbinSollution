using FakeFutbin.Models.Dto;

namespace FakeFutbin.Web.Services.Contracts;

public interface IManageScoutPlayersLocalStorageService
{
    Task<List<ScoutPlayerDto>> GetCollection();
    Task SaveColleciotn(List<ScoutPlayerDto> scoutPlayerDtos);
    Task RemoveCollection();
}
