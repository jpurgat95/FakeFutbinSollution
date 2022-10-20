using FakeFutbin.Models.Dto;

namespace FakeFutbin.Web.Services.Contracts;

public interface IUserService
{
    Task<List<UserPlayerDto>> GetPlayers(int coachId);
    Task<UserPlayerDto> AddPlayer(UserPlayerToAddDto coachPlayerToAddDto);
    Task<UserPlayerDto> DeletePlayer(int id);
    Task<UserPlayerDto> UpdateQty (UserPlayerQtyUpdateDto coachPlayerQtyUpdate);

    event Action<int> OnCoachChanged;
    void RaiseEventOnCoachChanged(int totalQty);
}
