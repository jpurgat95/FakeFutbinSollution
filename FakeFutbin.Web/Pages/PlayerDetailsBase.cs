using FakeFutbin.Models.Dto;
using FakeFutbin.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;

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
    public NavigationManager NavigationManager { get; set; }
    public PlayerDto Player { get; set; }
    public string ErrorMessage { get; set; }
    protected override async Task OnInitializedAsync()
    {
        try
        {
            Player = await PlayerService.GetPlayer(Id);
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
            var cartItemDto = await ScoutService.AddPlayer(scoutPlayerToAddDto);
            NavigationManager.NavigateTo("/Scout");
        }
        catch (Exception)
        {

            //Log Exception
        }
    }
}
