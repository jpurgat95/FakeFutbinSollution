using FakeFutbin.Models.Dto;
using FakeFutbin.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace FakeFutbin.Web.Pages;

public class PlayersByNationalityBase : ComponentBase
{
    [Parameter]
    public int NationalityId {get; set;}
    [Inject]
    public IPlayerService PlayerService { get; set; }
    public IEnumerable<PlayerDto> Players { get; set; }
    public string NationalityName { get; set; }
    public string ErrorMessage { get; set; }
    protected override async Task OnParametersSetAsync()
    {
        try
        {
            Players = await PlayerService.GetPlayersByNationality(NationalityId);
            if (Players != null && Players.Count()>0)
            {
                var playerDto = Players.FirstOrDefault(x=> x.NationalityId == NationalityId);
                if (playerDto != null)
                {
                    NationalityName = playerDto.NationalityName;
                }
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }
}
