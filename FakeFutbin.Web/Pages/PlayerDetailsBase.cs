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
    public IUserService CoachService { get; set; }
    [Inject]
    public IManagePlayersLocalStorageService ManagePlayersLocalStorageService { get; set; }
    [Inject]
    public IManageUserPlayersLocalStorageService ManageCoachPlayersLocalStorageService { get; set; }
    [Inject]
    public NavigationManager NavigationManager { get; set; }
    public PlayerDto Player { get; set; }
    public string ErrorMessage { get; set; }
    private List<UserPlayerDto> CoachPlayers { get; set; }
    protected override async Task OnInitializedAsync()
    {
        try
        {
            CoachPlayers = await ManageCoachPlayersLocalStorageService.GetCollection();
            Player = await GetPlayerById(Id);
        }
        catch (Exception ex)
        {

            ErrorMessage = ex.Message;
        }
    }

    protected async Task AddToCoach_Click(UserPlayerToAddDto coachPlayerToAddDto)
    {
        try
        {
            var coachPlayerDto = await CoachService.AddPlayer(coachPlayerToAddDto);
            if(coachPlayerDto != null)
            {
                CoachPlayers.Add(coachPlayerDto);
                await ManageCoachPlayersLocalStorageService.SaveColleciotn(CoachPlayers);
            }

            NavigationManager.NavigateTo("/Coach");
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
