namespace FakeFutbin.Web.Services.Contracts;

public interface IPositionService
{
    Task<IEnumerable<PositionDto>> GetPositions();
    Task<UserPlayerPositionDto> UpdatePosition(int id, UserPlayerPositionUpdateDto userPlayerPositionUpdateDto);
}
