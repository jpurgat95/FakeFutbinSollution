namespace FakeFutbin.Web.Services.Contracts;

public interface IUserService
{
    Task<List<UserPlayerDto>> GetPlayers(int userId);
    Task<UserPlayerDto> AddPlayer(UserPlayerToAddDto userPlayerToAddDto);
    Task<UserPlayerDto> DeletePlayer(int id);
    Task<UserPlayerDto> UpdateQty (UserPlayerQtyUpdateDto userPlayerQtyUpdate);
    Task<UserWalletDto> UpdateWallet(int id, UserWalletUpdateDto userWalletUpdateDto);
    Task<UserWalletDto> GetUser(int userId);
    Task<List<UserWalletDto>> GetUsers();

    event Action<int> OnUserChanged;
    event Action<int> OnWalletChanged;
    void RaiseEventOnUserChanged(int totalQty);
    void RaiseEventOnWalletChanged(int wallet);
    Task<UserPlayerPositionDto> UpdatePosition(int id, UserPlayerPositionUpdateDto userPlayerPositionUpdateDto);   
}
