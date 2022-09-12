using FakeFutbin.Models.Dto;
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
    public IScoutService ScoutService { get; set; }
    [Inject]
    public IManagePlayersLocalStorageService ManagePlayersLocalStorageService { get; set; }
    [Inject]
    public IManageScoutPlayersLocalStorageService ManageScoutPlayersLocalStorageService { get; set; }
    [Inject]
    public NavigationManager NavigationManager { get; set; }
    public PlayerDto Player { get; set; }
    public string ErrorMessage { get; set; }
    private List<ScoutPlayerDto> ScoutPlayers { get; set; }
    protected override async Task OnInitializedAsync()
    {
        try
        {
            ScoutPlayers = await ManageScoutPlayersLocalStorageService.GetCollection();
            Player = await GetPlayerById(Id);
        }
        catch (Exception ex)
        {

            ErrorMessage = ex.Message;
        }
    }

    protected async Task AddToScout_Click(ScoutPlayerToAddDto scoutPlayerToAddDto)
    {
        try
        {
            var scoutPlayerDto = await ScoutService.AddPlayer(scoutPlayerToAddDto);
            if(scoutPlayerDto != null)
            {
                ScoutPlayers.Add(scoutPlayerDto);
                await ManageScoutPlayersLocalStorageService.SaveColleciotn(ScoutPlayers);
            }

            NavigationManager.NavigateTo("/Scout");
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
