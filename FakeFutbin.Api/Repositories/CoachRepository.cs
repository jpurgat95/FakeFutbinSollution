using FakeFutbin.Api.Data;
using FakeFutbin.Api.Entities;
using FakeFutbin.Api.Repositories.Contracts;
using FakeFutbin.Models.Dto;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace FakeFutbin.Api.Repositories;

public class CoachRepository : ICoachRepository
{
    private readonly FakeFutbinDbContext _fakeFutbinDbContext;

    public CoachRepository(FakeFutbinDbContext fakeFutbinDbContext)
    {
        _fakeFutbinDbContext = fakeFutbinDbContext;
    }
    private async Task<bool> ScoutPlayerExists(int coachId, int playerId)
    {
        return await _fakeFutbinDbContext.CoachPlayers.AnyAsync(c => c.CoachId == coachId &&
                                                                    c.PlayerId == playerId);
    }
    public async Task<CoachPlayer> AddPlayer(CoachPlayerToAddDto coachPlayerToAddDto)
    {
        if (await ScoutPlayerExists(coachPlayerToAddDto.CoachId, coachPlayerToAddDto.PlayerId) == false)
        {
            var footballer = await (from player in _fakeFutbinDbContext.Players
                                    where player.Id == coachPlayerToAddDto.PlayerId
                                    select new CoachPlayer
                                    {
                                        CoachId = coachPlayerToAddDto.CoachId,
                                        PlayerId = player.Id,
                                        Qty = coachPlayerToAddDto.Qty

                                    }).SingleOrDefaultAsync();
            if (footballer != null)
            {
                var result = await _fakeFutbinDbContext.CoachPlayers.AddAsync(footballer);
                await _fakeFutbinDbContext.SaveChangesAsync();
                return result.Entity;
            }
        }
        return null;
    }

    public async Task<CoachPlayer> DeletePlayer(int id)
    {
        var player = await _fakeFutbinDbContext.CoachPlayers.FindAsync(id);

        if (player != null)
        {
            _fakeFutbinDbContext.CoachPlayers.Remove(player);
            await _fakeFutbinDbContext.SaveChangesAsync();
        }
        return player;
    }

    public async Task<CoachPlayer> GetPlayer(int id)
    {
        return await (from coach in _fakeFutbinDbContext.Coaches
                      join coachPlayer in _fakeFutbinDbContext.CoachPlayers
                      on coach.Id equals coachPlayer.CoachId
                      where coachPlayer.Id == id
                      select new CoachPlayer
                      {
                          Id = coachPlayer.Id,
                          PlayerId = coachPlayer.PlayerId,
                          Qty = coachPlayer.Qty,
                          CoachId = coachPlayer.CoachId
                      }).SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<CoachPlayer>> GetPlayers(int coachId)
    {
        return await (from coach in _fakeFutbinDbContext.Coaches
                      join coachPlayer in _fakeFutbinDbContext.CoachPlayers
                      on coach.Id equals coachPlayer.CoachId
                      where coach.Id == coachId
                      select new CoachPlayer
                      {
                          Id = coachPlayer.Id,
                          PlayerId = coachPlayer.PlayerId,
                          Qty = coachPlayer.Qty,
                          CoachId= coachPlayer.CoachId
                      }).ToListAsync();
    }

    public async Task<CoachPlayer> UpddateQty(int id, CoachPlayerQtyUpdateDto coachPlayerQtyUpdateDto)
    {
        var player = await _fakeFutbinDbContext.CoachPlayers.FindAsync(id);
        if (player != null)
        {
            player.Qty = coachPlayerQtyUpdateDto.Qty;
            await _fakeFutbinDbContext.SaveChangesAsync();
            return player;
        }
        return null;
    }
}
