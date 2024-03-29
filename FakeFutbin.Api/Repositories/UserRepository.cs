﻿namespace FakeFutbin.Api.Repositories;

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
                                        Qty = userPlayerToAddDto.Qty,
                                        Position = userPlayerToAddDto.Position,

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
                          Position = userPlayer.Position,
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
                          Position = userPlayer.Position,
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
    public async Task<User> UpdateWallet(int id, UserWalletUpdateDto userWalletUpdateDto)
    {
        var user = await _fakeFutbinDbContext.Users.FindAsync(id);
        if(user != null)
        {
            user.Wallet = userWalletUpdateDto.Wallet;
            await _fakeFutbinDbContext.SaveChangesAsync();
            return user;
        }
        return null;
    }
    public async Task<User> GetUser(int id)
    {
        var user = await _fakeFutbinDbContext.Users
                   .SingleOrDefaultAsync(p => p.Id == id);
        return user;
    }
    public async Task<IEnumerable<User>> GetUsers()
    {
        var users = await _fakeFutbinDbContext.Users
                   .ToListAsync();
        return users;
    }
}
