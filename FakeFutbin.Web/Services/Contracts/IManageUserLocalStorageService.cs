namespace FakeFutbin.Web.Services.Contracts;

public interface IManageUserLocalStorageService
{
    Task<List<UserWalletDto>> GetCollection();
    Task SaveColleciotn(List<UserWalletDto> userDtos);
    Task RemoveCollection();
}
