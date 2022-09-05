using FakeFutbin.Api.Entities;

namespace FakeFutbin.Api.Repositories.Contracts;

public interface IPlayerRepository
{
    Task<IEnumerable<Player>> GetPlayers();
    Task<IEnumerable<PlayerNationality>> GetNationalities();
    Task<Player> GetPlayer(int id);
    Task<PlayerNationality> GetNationality(int id);
    Task<IEnumerable<Player>> GetPlayersByNationality(int id);


}
