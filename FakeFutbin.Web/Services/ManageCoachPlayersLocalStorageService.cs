using Blazored.LocalStorage;
using FakeFutbin.Models.Dto;
using FakeFutbin.Web.Services.Contracts;

namespace FakeFutbin.Web.Services;

public class ManageCoachPlayersLocalStorageService : IManageCoachPlayersLocalStorageService
{
    private readonly ILocalStorageService _localStorageService;
    private readonly ICoachService _coachService;

    const string key = "CoachPlayerCollection";

    public ManageCoachPlayersLocalStorageService(ILocalStorageService localStorageService,
                                                ICoachService coachService)
    {
        _localStorageService = localStorageService;
        _coachService = coachService;
    }
    public async Task<List<CoachPlayerDto>> GetCollection()
    {
        return await _localStorageService.GetItemAsync<List<CoachPlayerDto>>(key)
                ?? await AddCollection();
    }

    public async Task RemoveCollection()
    {
        await _localStorageService.RemoveItemAsync(key);
    }

    public async Task SaveColleciotn(List<CoachPlayerDto> coachPlayerDtos)
    {
        await _localStorageService.SetItemAsync(key, coachPlayerDtos);
    }

    private async Task<List<CoachPlayerDto>> AddCollection()
    {
        var coachPlayerCollection = await _coachService.GetPlayers(HardCoded.CoachId);

        if(coachPlayerCollection != null)
        {
            await _localStorageService.SetItemAsync(key, coachPlayerCollection);
        }
        return coachPlayerCollection;
    }
}
