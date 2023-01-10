using FakeFutbin.Api.Entities;
using FakeFutbin.Models.Dto;

namespace FakeFutbin.Api.Repositories.Contracts;

public interface IUserRepository
{
    Task<UserPlayer> AddPlayer(UserPlayerToAddDto userPlayerToAddDto);
    Task<UserPlayer> UpddateQty(int id, UserPlayerQtyUpdateDto userPlayerQtyUpdateDto);
    Task<UserPlayer> DeletePlayer(int id);
    Task<UserPlayer> GetPlayer(int id);
    Task<IEnumerable<UserPlayer>> GetPlayers(int userId);
    Task<User> UpdateWallet(int id, UserWalletUpdateDto userWalletUpdateDto);
    Task <User> GetUser(int id);
    Task<IEnumerable<User>> GetUsers();
    Task<UserPlayer> UpdatePosition(int id, UserPlayerPositionUpdateDto userPlayerPositionUpdateDto);
}
