using FakeFutbin.Models.Dto;
using FakeFutbin.Web.Services;
using FakeFutbin.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.VisualBasic;

namespace FakeFutbin.Web.Pages;
public class PlayerDetailsBase : ComponentBase
{
    [Parameter]
    public int Id { get; set; }
    [Inject]
    public IPlayerService PlayerService { get; set; }
    [Inject]
    public IUserService UserService { get; set; }
    [Inject]
    public IManagePlayersLocalStorageService ManagePlayersLocalStorageService { get; set; }
    [Inject]
    public IManageUserPlayersLocalStorageService ManageUserPlayersLocalStorageService { get; set; }
    [Inject]
    public NavigationManager NavigationManager { get; set; }
    public PlayerDto Player { get; set; }
    public string ErrorMessage { get; set; }
    private List<UserPlayerDto> UserPlayers { get; set; }
    protected override async Task OnInitializedAsync()
    {
        try
        {
            UserPlayers = await ManageUserPlayersLocalStorageService.GetCollection();
            Player = await GetPlayerById(Id);
        }
        catch (Exception ex)
        {

            ErrorMessage = ex.Message;
        }
    }

    protected async Task AddToUser_Click(UserPlayerToAddDto userPlayerToAddDto)
    {
        try
        {
            var userPlayerDto = await UserService.AddPlayer(userPlayerToAddDto);
            if(userPlayerDto != null)
            {
                UserPlayers.Add(userPlayerDto);
                await ManageUserPlayersLocalStorageService.SaveColleciotn(UserPlayers);
            }

            NavigationManager.NavigateTo("/User");
        }
        catch (Exception)
        {

            //Log Exception
        }
    }

    private async Task<PlayerDto> GetPlayerById(int id)
    {
        var playerDtos = await ManagePlayersLocalStorageService.GetCollection();

        if(playerDtos != null)
        {
            return playerDtos.SingleOrDefault(p => p.Id == id);
        }
        return null;
    }
}
