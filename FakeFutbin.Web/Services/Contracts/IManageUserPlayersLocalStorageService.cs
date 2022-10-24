using FakeFutbin.Models.Dto;

namespace FakeFutbin.Web.Services.Contracts;

public interface IManageUserPlayersLocalStorageService
{
    Task<List<UserPlayerDto>> GetCollection();
    Task SaveColleciotn(List<UserPlayerDto> userPlayerDtos);
    Task RemoveCollection();
}
