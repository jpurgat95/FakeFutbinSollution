using FakeFutbin.Api.Entities;
using FakeFutbin.Models.Dto;

namespace FakeFutbin.Api.Repositories.Contracts;

public interface IScoutRepository
{
    Task<ScoutPlayer> AddPlayer (ScoutPlayerToAddDto scoutPlayerToAddDto);
    Task<ScoutPlayer> UpddateQty (int id, ScoutPlayerQtyUpdateDto scoutPlayerQtyUpdateDto);
    Task<ScoutPlayer> DeletePlayer(int id);
    Task<ScoutPlayer> GetPlayer (int id);
    Task<IEnumerable<ScoutPlayer>> GetPlayers (int coachId);
}
