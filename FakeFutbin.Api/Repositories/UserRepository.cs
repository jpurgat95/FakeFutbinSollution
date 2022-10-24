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
    private async Task<bool> UserPlayerExists(int userId, int playerId)
    {
        return await _fakeFutbinDbContext.UserPlayers.AnyAsync(c => c.UserId == userId &&
                                                                    c.PlayerId == playerId);
    }
    public async Task<UserPlayer> AddPlayer(UserPlayerToAddDto userPlayerToAddDto)
    {
        if (await UserPlayerExists(userPlayerToAddDto.UserId, userPlayerToAddDto.PlayerId) == false)
        {
            var footballer = await (from player in _fakeFutbinDbContext.Players
                                    where player.Id == userPlayerToAddDto.PlayerId
                                    select new UserPlayer
                                    {
                                        UserId = userPlayerToAddDto.UserId,
                                        PlayerId = player.Id,
                                        Qty = userPlayerToAddDto.Qty

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
        return await (from user in _fakeFutbinDbContext.Users
                      join userPlayer in _fakeFutbinDbContext.UserPlayers
                      on user.Id equals userPlayer.UserId
                      where userPlayer.Id == id
                      select new UserPlayer
                      {
                          Id = userPlayer.Id,
                          PlayerId = userPlayer.PlayerId,
                          Qty = userPlayer.Qty,
                          UserId = userPlayer.UserId
                      }).SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<UserPlayer>> GetPlayers(int userId)
    {
        return await (from user in _fakeFutbinDbContext.Users
                      join userPlayer in _fakeFutbinDbContext.UserPlayers
                      on user.Id equals userPlayer.UserId
                      where user.Id == userId
                      select new UserPlayer
                      {
                          Id = userPlayer.Id,
                          PlayerId = userPlayer.PlayerId,
                          Qty = userPlayer.Qty,
                          UserId= userPlayer.UserId
                      }).ToListAsync();
    }

    public async Task<UserPlayer> UpddateQty(int id, UserPlayerQtyUpdateDto userPlayerQtyUpdateDto)
    {
        var player = await _fakeFutbinDbContext.UserPlayers.FindAsync(id);
        if (player != null)
        {
            player.Qty = userPlayerQtyUpdateDto.Qty;
            await _fakeFutbinDbContext.SaveChangesAsync();
            return player;
        }
        return null;
    }
}
