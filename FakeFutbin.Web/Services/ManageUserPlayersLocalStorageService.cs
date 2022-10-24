using Blazored.LocalStorage;
using FakeFutbin.Models.Dto;
using FakeFutbin.Web.Pages;
using FakeFutbin.Web.Services.Contracts;

namespace FakeFutbin.Web.Services;

public class ManageUserPlayersLocalStorageService : IManageUserPlayersLocalStorageService
{
    private readonly ILocalStorageService _localStorageService;
    private readonly IUserService _userService;
    const string key = "UserPlayerCollection";
    public ManageUserPlayersLocalStorageService(ILocalStorageService localStorageService,
                                                IUserService userService)
    {
        _localStorageService = localStorageService;
        _userService = userService;
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

    public async Task SaveColleciotn(List<UserPlayerDto> userPlayerDtos)
    {
        await _localStorageService.SetItemAsync(key, userPlayerDtos);
    }

    private async Task<List<UserPlayerDto>> AddCollection()
    {
        var userPlayerCollection = await _userService.GetPlayers(HardCoded.UserId);

        if(userPlayerCollection != null)
        {
            await _localStorageService.SetItemAsync(key, userPlayerCollection);
        }
        return userPlayerCollection;
    }
}
