using FakeFutbin.Models.Dto;
using FakeFutbin.Web.Services;
using FakeFutbin.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace FakeFutbin.Web.Pages;

public class PlayersBase : ComponentBase
{
    [Inject]
    public IPlayerService PlayerService { get; set; }
    [Inject]
    public IScoutService ScoutService { get; set; }
    public IEnumerable<PlayerDto> Players { get; set; }
    [Inject]
    public NavigationManager NavigationManager { get; set; }
    public string ErrorMessage { get; set; }
    protected override async Task OnInitializedAsync()
    {
        try
        {
            Players = await PlayerService.GetPlayers();
            var scoutPlayers = await ScoutService.GetPlayers(HardCoded.ScoutId);
            var totalQty = scoutPlayers.Sum(i=>i.Qty);

            ScoutService.RaiseEventOnScoutChanged(totalQty);
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }
    protected IOrderedEnumerable<IGrouping<int, PlayerDto>> GetGroupedPlayersByNationality()
    {
        return from player in Players
               group player by player.NationalityId into playerByNatGroup
               orderby playerByNatGroup.Key
               select playerByNatGroup;
    }

    protected string GetNationalityName(IGrouping<int, PlayerDto> groupedPlayerDto)
    {
        return groupedPlayerDto.FirstOrDefault(pg=>pg.NationalityId == groupedPlayerDto.Key)
            .NationalityName;
    }
}
