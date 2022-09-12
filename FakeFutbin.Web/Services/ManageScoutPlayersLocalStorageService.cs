using Blazored.LocalStorage;
using FakeFutbin.Models.Dto;
using FakeFutbin.Web.Services.Contracts;

namespace FakeFutbin.Web.Services;

public class ManageScoutPlayersLocalStorageService : IManageScoutPlayersLocalStorageService
{
    private readonly ILocalStorageService _localStorageService;
    private readonly IScoutService _scoutService;

    const string key = "ScoutPlayerCollection";

    public ManageScoutPlayersLocalStorageService(ILocalStorageService localStorageService,
                                                IScoutService scoutService)
    {
        _localStorageService = localStorageService;
        _scoutService = scoutService;
    }
    public async Task<List<ScoutPlayerDto>> GetCollection()
    {
        return await _localStorageService.GetItemAsync<List<ScoutPlayerDto>>(key)
                ?? await AddCollection();
    }

    public async Task RemoveCollection()
    {
        await _localStorageService.RemoveItemAsync(key);
    }

    public async Task SaveColleciotn(List<ScoutPlayerDto> scoutPlayerDtos)
    {
        await _localStorageService.SetItemAsync(key, scoutPlayerDtos);
    }

    private async Task<List<ScoutPlayerDto>> AddCollection()
    {
        var scoutPlayerCollection = await _scoutService.GetPlayers(HardCoded.CoachId);

        if(scoutPlayerCollection != null)
        {
            await _localStorageService.SetItemAsync(key, scoutPlayerCollection);
        }
        return scoutPlayerCollection;
    }
}
