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
}
