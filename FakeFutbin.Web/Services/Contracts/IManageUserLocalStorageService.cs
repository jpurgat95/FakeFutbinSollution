using FakeFutbin.Models.Dto;

namespace FakeFutbin.Web.Services.Contracts;

public interface IManageUserLocalStorageService
{
    Task<List<UserDto2>> GetCollection();
    Task SaveColleciotn(List<UserDto2> userDtos);
    Task RemoveCollection();
}
