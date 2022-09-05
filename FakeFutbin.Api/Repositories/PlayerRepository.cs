using FakeFutbin.Api.Data;
using FakeFutbin.Api.Entities;
using FakeFutbin.Api.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace FakeFutbin.Api.Repositories;

public class PlayerRepository : IPlayerRepository
{
    private readonly FakeFutbinDbContext _fakeFutbinDbContext;

    public PlayerRepository(FakeFutbinDbContext fakeFutbinDbContext)
    {
        _fakeFutbinDbContext = fakeFutbinDbContext;
    }
    public async Task<IEnumerable<PlayerNationality>> GetNationalities()
    {
        var nationalities = await _fakeFutbinDbContext.PlayerNationalities.ToListAsync();
        return nationalities;
    }

    public Task<PlayerNationality> GetNationality(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Player> GetPlayer(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Player>> GetPlayers()
    {
        var players = await _fakeFutbinDbContext.Players.ToListAsync();
        return players;
    }

    public Task<IEnumerable<Player>> GetPlayersByNationality(int id)
    {
        throw new NotImplementedException();
    }
}
