using FakeFutbin.Models.Dto;

namespace FakeFutbin.Web.Services.Contracts;

public interface IPlayerService
{
    Task<IEnumerable<PlayerDto>> GetPlayers();
    Task<PlayerDto> GetPlayer(int id);
    Task<IEnumerable<PlayerNationalityDto>> GetPlayerNationalities(int id);
    Task<IEnumerable<PlayerDto>> GetPlayersByNationality(int nationalityId);
}
