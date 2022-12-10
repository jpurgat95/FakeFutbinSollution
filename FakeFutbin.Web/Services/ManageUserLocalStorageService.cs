using Blazored.LocalStorage;
using FakeFutbin.Models.Dto;
using FakeFutbin.Web.Services.Contracts;

namespace FakeFutbin.Web.Services;

public class ManageUserLocalStorageService : IManageUserLocalStorageService
{
    private readonly ILocalStorageService _localStorage;
    private readonly IUserService _userService;
    const string key = "UsersCollection";

    public ManageUserLocalStorageService(ILocalStorageService localStorage,
                                         IUserService userService)
    {
        _localStorage = localStorage;
        _userService = userService;
    }
    public async Task<List<UserDto2>> GetCollection()
    {
        return await _localStorage.GetItemAsync<List<UserDto2>>(key)
        ?? await AddCollection();
    }

    public async Task RemoveCollection()
    {
        await _localStorage.RemoveItemAsync(key);
    }

    public async Task SaveColleciotn(List<UserDto2> userDtos)
    {
        await _localStorage.SetItemAsync(key, userDtos);
    }
    private async Task<List<UserDto2>> AddCollection()
    {
        var userCollection = await _userService.GetUsers();

        if (userCollection != null)
        {
            await _localStorage.SetItemAsync(key, userCollection);
        }
        return userCollection;
    }
}
