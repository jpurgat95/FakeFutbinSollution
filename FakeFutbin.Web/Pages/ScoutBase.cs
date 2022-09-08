using FakeFutbin.Models.Dto;
using FakeFutbin.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace FakeFutbin.Web.Pages;

public class ScoutBase : ComponentBase
{
    [Inject]
    public IScoutService ScoutService { get; set; }
    public IEnumerable<ScoutPlayerDto> ScoutPlayers { get; set; }
    public string ErrorMessage { get; set; }    
    protected override async Task OnInitializedAsync()
    {
        try
        {
            ScoutPlayers = await ScoutService.GetPlayers(HardCoded.CoachId);
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }

}
