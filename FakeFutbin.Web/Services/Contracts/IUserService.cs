using FakeFutbin.Models.Dto;

namespace FakeFutbin.Web.Services.Contracts;

public interface IUserService
{
    Task<List<UserPlayerDto>> GetPlayers(int userId);
    Task<UserPlayerDto> AddPlayer(UserPlayerToAddDto userPlayerToAddDto);
    Task<UserPlayerDto> DeletePlayer(int id);
    Task<UserPlayerDto> UpdateQty (UserPlayerQtyUpdateDto userPlayerQtyUpdate);

    event Action<int> OnUserChanged;
    void RaiseEventOnUserChanged(int totalQty);
}
