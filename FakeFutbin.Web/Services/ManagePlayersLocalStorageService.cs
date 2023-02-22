namespace FakeFutbin.Web.Services;

public class ManagePlayersLocalStorageService : IManagePlayersLocalStorageService
{
    private readonly ILocalStorageService _localStorageService;
    private readonly IPlayerService _playerService;

    private const string key = "PlayerCollection";

    public ManagePlayersLocalStorageService(ILocalStorageService localStorageService,
                                            IPlayerService playerService)
    {
        _localStorageService = localStorageService;
        _playerService = playerService;
    }
    public async Task<IEnumerable<PlayerDto>> GetCollection()
    {
        return await _localStorageService.GetItemAsync<IEnumerable<PlayerDto>>(key)
                    ?? await AddCollection();
    }

    public async Task RemoveCollection()
    {
        await _localStorageService.RemoveItemAsync(key);
    }

    private async Task<IEnumerable<PlayerDto>> AddCollection()
    {
        var playerCollection = await _playerService.GetPlayers();

        if(playerCollection != null)
        {
            await _localStorageService.SetItemAsync(key, playerCollection);
        }
        return playerCollection;
    }
}
