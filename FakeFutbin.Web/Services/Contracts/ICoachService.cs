using FakeFutbin.Models.Dto;

namespace FakeFutbin.Web.Services.Contracts;

public interface ICoachService
{
    Task<List<CoachPlayerDto>> GetPlayers(int coachId);
    Task<CoachPlayerDto> AddPlayer(CoachPlayerToAddDto coachPlayerToAddDto);
    Task<CoachPlayerDto> DeletePlayer(int id);
    Task<CoachPlayerDto> UpdateQty (CoachPlayerQtyUpdateDto coachPlayerQtyUpdate);

    event Action<int> OnCoachChanged;
    void RaiseEventOnCoachChanged(int totalQty);
}
