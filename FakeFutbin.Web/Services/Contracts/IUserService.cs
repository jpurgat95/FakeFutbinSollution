using FakeFutbin.Models.Dto;

namespace FakeFutbin.Web.Services.Contracts;

public interface IUserService
{
    Task<List<UserPlayerDto>> GetPlayers(int userId);
    Task<UserPlayerDto> AddPlayer(UserPlayerToAddDto userPlayerToAddDto);
    Task<UserPlayerDto> DeletePlayer(int id);
    Task<UserPlayerDto> UpdateQty (UserPlayerQtyUpdateDto userPlayerQtyUpdate);
    Task<UserDto2> UpdateWallet(int id, UserWalletUpdateDto userWalletUpdateDto);
    Task<UserDto2> GetUser(int userId);
    Task<List<UserDto2>> GetUsers();

    event Action<int> OnUserChanged;
    event Action<int> OnWalletChanged;
    void RaiseEventOnUserChanged(int totalQty);
    void RaiseEventOnWalletChanged(int wallet);
}
