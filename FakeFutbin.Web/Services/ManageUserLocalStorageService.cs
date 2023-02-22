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
    public async Task<List<UserWalletDto>> GetCollection()
    {
        return await _localStorage.GetItemAsync<List<UserWalletDto>>(key)
        ?? await AddCollection();
    }

    public async Task RemoveCollection()
    {
        await _localStorage.RemoveItemAsync(key);
    }

    public async Task SaveColleciotn(List<UserWalletDto> userDtos)
    {
        await _localStorage.SetItemAsync(key, userDtos);
    }
    private async Task<List<UserWalletDto>> AddCollection()
    {
        var userCollection = await _userService.GetUsers();

        if (userCollection != null)
        {
            await _localStorage.SetItemAsync(key, userCollection);
        }
        return userCollection;
    }
}
