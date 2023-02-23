namespace FakeFutbin.Api.Repositories.Contracts;

public interface IPositionRepository
{
    Task<IEnumerable<Position>> GetPositions();
    Task<UserPlayer> UpdatePosition(int id, UserPlayerPositionUpdateDto userPlayerPositionUpdateDto);
}