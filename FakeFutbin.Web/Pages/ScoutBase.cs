using FakeFutbin.Models.Dto;
using FakeFutbin.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace FakeFutbin.Web.Pages;

public class ScoutBase : ComponentBase
{
    [Inject]
    public IScoutService ScoutService { get; set; }
    public List<ScoutPlayerDto> ScoutPlayers { get; set; }
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

    protected async Task DeleteScoutPlayer_Click(int id)
    {
        var scoutPlayerDto = await ScoutService.DeletePlayer(id);

        RemoveScoutPlayer(id);
    }
    private ScoutPlayerDto GetScoutPlayer(int id)
    {
        return ScoutPlayers.FirstOrDefault(x => x.Id == id);
    }
    private void RemoveScoutPlayer(int id)
    {
        var scoutPlayerDto = GetScoutPlayer(id);
        ScoutPlayers.Remove(scoutPlayerDto);
    }
}
