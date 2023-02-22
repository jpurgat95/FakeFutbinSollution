namespace FakeFutbin.Web.Services.Contracts;

public interface IManagePlayersLocalStorageService
{
    Task<IEnumerable<PlayerDto>> GetCollection();
    Task RemoveCollection();
}
