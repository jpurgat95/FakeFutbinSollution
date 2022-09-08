using FakeFutbin.Api.Data;
using FakeFutbin.Api.Entities;
using FakeFutbin.Api.Repositories.Contracts;
using FakeFutbin.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace FakeFutbin.Api.Repositories;

public class ScoutRepository : IScoutRepository
{
    private readonly FakeFutbinDbContext _fakeFutbinDbContext;

    public ScoutRepository(FakeFutbinDbContext fakeFutbinDbContext)
    {
        _fakeFutbinDbContext = fakeFutbinDbContext;
    }
    private async Task<bool> ScoutPlayerExists (int scoutId, int playerId)
    {
        return await _fakeFutbinDbContext.ScoutPlayers.AnyAsync(c=> c.ScoutId == scoutId && 
                                                                    c.PlayerId == playerId);
    }
    public async Task<ScoutPlayer> AddPlayer(ScoutPlayerToAddDto scoutPlayerToAddDto)
    {
        if(await ScoutPlayerExists(scoutPlayerToAddDto.ScoutId, scoutPlayerToAddDto.PlayerId) == false)
        {
            var footballer = await (from player in _fakeFutbinDbContext.Players
                                    where player.Id == scoutPlayerToAddDto.PlayerId
                                    select new ScoutPlayer
                                    {
                                        ScoutId = scoutPlayerToAddDto.ScoutId,
                                        PlayerId = player.Id,
                                        Qty = scoutPlayerToAddDto.Qty

                                    }).SingleOrDefaultAsync();
            if (footballer != null)
            {
                var result = await _fakeFutbinDbContext.ScoutPlayers.AddAsync(footballer);
                await _fakeFutbinDbContext.SaveChangesAsync();
                return result.Entity;
            }
        }
        return null;
    }

    public Task<ScoutPlayer> DeletePlayer(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ScoutPlayer> GetPlayer(int id)
    {
        return await (from scout in _fakeFutbinDbContext.Scouts
                      join scoutPlayer in _fakeFutbinDbContext.ScoutPlayers
                      on scout.Id equals scoutPlayer.ScoutId
                      where scoutPlayer.Id == id
                      select new ScoutPlayer
                      {
                        Id = scoutPlayer.Id,
                        PlayerId = scoutPlayer.PlayerId,
                        Qty = scoutPlayer.Qty,
                        ScoutId = scoutPlayer.ScoutId
                      }).SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<ScoutPlayer>> GetPlayers(int coachId)
    {
        return await (from scout in _fakeFutbinDbContext.Scouts
                      join scoutPlayer in _fakeFutbinDbContext.ScoutPlayers
                      on scout.Id equals scoutPlayer.ScoutId
                      where scout.CoachId == coachId
                      select new ScoutPlayer
                      {
                          Id = scoutPlayer.Id,
                          PlayerId = scoutPlayer.PlayerId,
                          Qty = scoutPlayer.Qty,
                          ScoutId= scoutPlayer.ScoutId
                      }).ToListAsync();
    }

    public Task<ScoutPlayer> UpddateQty(int id, ScoutPlayerQtyUpdateDto scoutPlayerQtyUpdateDto)
    {
        throw new NotImplementedException();
    }
}
