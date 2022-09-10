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

    public async Task<PlayerNationality> GetNationality(int id)
    {
        var nationality = await _fakeFutbinDbContext.PlayerNationalities.SingleOrDefaultAsync(c => c.Id ==id);
        return nationality;
    }

    public async Task<Player> GetPlayer(int id)
    {
        var player = await _fakeFutbinDbContext.Players.FindAsync(id);
        return player;
    }

    public async Task<IEnumerable<Player>> GetPlayers()
    {
        var players = await _fakeFutbinDbContext.Players.ToListAsync();
        return players;
    }

    public async Task<IEnumerable<Player>> GetPlayersByCategory(int id)
    {
        var players = await (from player in _fakeFutbinDbContext.Players
                             where player.NationalityId == id
                             select player).ToListAsync();
        return players;
    }
}
