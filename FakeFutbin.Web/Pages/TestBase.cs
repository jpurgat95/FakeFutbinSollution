using FakeFutbin.Models.Dto;
using FakeFutbin.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace FakeFutbin.Web.Pages;

public class TestBase : ComponentBase
{
    [Inject]
    public IUserIdService UserIdService { get; set; }
    [Inject]
    public IManageUserLocalStorageService ManageUserLocalStorageService { get; set; }
    [Inject]
    public IManageUserPlayersLocalStorageService ManageUserPlayersLocalStorageService { get; set; }
    public List<UserPlayerDto> UserPlayers { get; set; }
    public int UserId { get; set; }
    public string ErrorMessage { get; set; }
    public IEnumerable<UserPlayerDto> UserPlayersRadzen{ get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            UserPlayers = await ManageUserPlayersLocalStorageService.GetCollection();
            UserId = await UserIdService.GetUserId();

            var userPlayers = UserPlayers.Where(p => p.UserId == UserId).ToList();
            UserPlayersRadzen = userPlayers;
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }

    public void OnChange(object value)
    {
        var str = value is IEnumerable<object> ? string.Join(", ", (IEnumerable<object>)value) : value;

        Console.WriteLine($"Value changed to {str}");
    }
    public async Task<IEnumerable<UserPlayerDto>> GetUserPlayers()
    {
        UserPlayers = await ManageUserPlayersLocalStorageService.GetCollection();
        UserId = await UserIdService.GetUserId();

        var userPlayers = UserPlayers.Where(p => p.UserId == UserId).ToList();
        return userPlayers;
    }
}
