using FakeFutbin.Api.Data;
using FakeFutbin.Api.Entities;
using FakeFutbin.Api.Repositories.Contracts;
using FakeFutbin.Models.Dto;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace FakeFutbin.Api.Repositories;

public class UserRepository : IUserRepository
{
    private readonly FakeFutbinDbContext _fakeFutbinDbContext;

    public UserRepository(FakeFutbinDbContext fakeFutbinDbContext)
    {
        _fakeFutbinDbContext = fakeFutbinDbContext;
    }
    private async Task<bool> ScoutPlayerExists(int coachId, int playerId)
    {
        return await _fakeFutbinDbContext.UserPlayers.AnyAsync(c => c.UserId == coachId &&
                                                                    c.PlayerId == playerId);
    }
    public async Task<UserPlayer> AddPlayer(UserPlayerToAddDto coachPlayerToAddDto)
    {
        if (await ScoutPlayerExists(coachPlayerToAddDto.UserId, coachPlayerToAddDto.PlayerId) == false)
        {
            var footballer = await (from player in _fakeFutbinDbContext.Players
                                    where player.Id == coachPlayerToAddDto.PlayerId
                                    select new UserPlayer
                                    {
                                        UserId = coachPlayerToAddDto.UserId,
                                        PlayerId = player.Id,
                                        Qty = coachPlayerToAddDto.Qty

                                    }).SingleOrDefaultAsync();
            if (footballer != null)
            {
                var result = await _fakeFutbinDbContext.UserPlayers.AddAsync(footballer);
                await _fakeFutbinDbContext.SaveChangesAsync();
                return result.Entity;
            }
        }
        return null;
    }

    public async Task<UserPlayer> DeletePlayer(int id)
    {
        var player = await _fakeFutbinDbContext.UserPlayers.FindAsync(id);

        if (player != null)
        {
            _fakeFutbinDbContext.UserPlayers.Remove(player);
            await _fakeFutbinDbContext.SaveChangesAsync();
        }
        return player;
    }

    public async Task<UserPlayer> GetPlayer(int id)
    {
        return await (from coach in _fakeFutbinDbContext.Users
                      join coachPlayer in _fakeFutbinDbContext.UserPlayers
                      on coach.Id equals coachPlayer.UserId
                      where coachPlayer.Id == id
                      select new UserPlayer
                      {
                          Id = coachPlayer.Id,
                          PlayerId = coachPlayer.PlayerId,
                          Qty = coachPlayer.Qty,
                          UserId = coachPlayer.UserId
                      }).SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<UserPlayer>> GetPlayers(int coachId)
    {
        return await (from coach in _fakeFutbinDbContext.Users
                      join coachPlayer in _fakeFutbinDbContext.UserPlayers
                      on coach.Id equals coachPlayer.UserId
                      where coach.Id == coachId
                      select new UserPlayer
                      {
                          Id = coachPlayer.Id,
                          PlayerId = coachPlayer.PlayerId,
                          Qty = coachPlayer.Qty,
                          UserId= coachPlayer.UserId
                      }).ToListAsync();
    }

    public async Task<UserPlayer> UpddateQty(int id, UserPlayerQtyUpdateDto coachPlayerQtyUpdateDto)
    {
        var player = await _fakeFutbinDbContext.UserPlayers.FindAsync(id);
        if (player != null)
        {
            player.Qty = coachPlayerQtyUpdateDto.Qty;
            await _fakeFutbinDbContext.SaveChangesAsync();
            return player;
        }
        return null;
    }
}
