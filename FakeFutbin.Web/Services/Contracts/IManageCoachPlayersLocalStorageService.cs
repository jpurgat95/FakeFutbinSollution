using FakeFutbin.Models.Dto;

namespace FakeFutbin.Web.Services.Contracts;

public interface IManageCoachPlayersLocalStorageService
{
    Task<List<CoachPlayerDto>> GetCollection();
    Task SaveColleciotn(List<CoachPlayerDto> coachPlayerDtos);
    Task RemoveCollection();
}
