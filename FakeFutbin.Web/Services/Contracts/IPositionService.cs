using FakeFutbin.Models.Dto;

namespace FakeFutbin.Web.Services.Contracts;

public interface IPositionService
{
    Task<IEnumerable<PositionDto>> GetPositions();
}
