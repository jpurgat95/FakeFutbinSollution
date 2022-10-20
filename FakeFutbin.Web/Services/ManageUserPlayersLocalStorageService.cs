using Blazored.LocalStorage;
using FakeFutbin.Models.Dto;
using FakeFutbin.Web.Services.Contracts;

namespace FakeFutbin.Web.Services;

public class ManageUserPlayersLocalStorageService : IManageUserPlayersLocalStorageService
{
    private readonly ILocalStorageService _localStorageService;
    private readonly IUserService _coachService;

    const string key = "CoachPlayerCollection";

    public ManageUserPlayersLocalStorageService(ILocalStorageService localStorageService,
                                                IUserService coachService)
    {
        _localStorageService = localStorageService;
        _coachService = coachService;
    }
    public async Task<List<UserPlayerDto>> GetCollection()
    {
        return await _localStorageService.GetItemAsync<List<UserPlayerDto>>(key)
                ?? await AddCollection();
    }

    public async Task RemoveCollection()
    {
        await _localStorageService.RemoveItemAsync(key);
    }

    public async Task SaveColleciotn(List<UserPlayerDto> coachPlayerDtos)
    {
        await _localStorageService.SetItemAsync(key, coachPlayerDtos);
    }

    private async Task<List<UserPlayerDto>> AddCollection()
    {
        var coachPlayerCollection = await _coachService.GetPlayers(HardCoded.UserId);

        if(coachPlayerCollection != null)
        {
            await _localStorageService.SetItemAsync(key, coachPlayerCollection);
        }
        return coachPlayerCollection;
    }
}
