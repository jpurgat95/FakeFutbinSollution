using FakeFutbin.Api.Entities;
using FakeFutbin.Models.Dto;

namespace FakeFutbin.Api.Repositories.Contracts;

public interface ICoachRepository
{
    Task<CoachPlayer> AddPlayer(CoachPlayerToAddDto coachPlayerToAddDto);
    Task<CoachPlayer> UpddateQty(int id, CoachPlayerQtyUpdateDto coachPlayerQtyUpdateDto);
    Task<CoachPlayer> DeletePlayer(int id);
    Task<CoachPlayer> GetPlayer(int id);
    Task<IEnumerable<CoachPlayer>> GetPlayers(int coachId);
}
