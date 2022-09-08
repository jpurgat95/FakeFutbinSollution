using FakeFutbin.Models.Dto;

namespace FakeFutbin.Web.Services.Contracts;

public interface IScoutService
{
    Task<IEnumerable<ScoutPlayerDto>> GetPlayers(int coachId);
    Task<ScoutPlayerDto> AddPlayer(ScoutPlayerToAddDto scoutPlayerToAddDto);
}
