using FakeFutbin.Models.Dto;
using FakeFutbin.Web.Services;
using FakeFutbin.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System.Runtime.InteropServices;

namespace FakeFutbin.Web.Pages;

public class PlayersBase : ComponentBase
{
    [Inject]
    public IPlayerService PlayerService { get; set; }
    [Inject]
    public ICoachService CoachService { get; set; }
    [Inject]
    public IManagePlayersLocalStorageService ManagePlayersLocalStorageService { get; set; }
    [Inject]
    public IManageCoachPlayersLocalStorageService ManageCoachPlayersLocalStorageService { get; set; }
    public IEnumerable<PlayerDto> Players { get; set; }
    [Inject]
    public NavigationManager NavigationManager { get; set; }
    public string ErrorMessage { get; set; }
    protected override async Task OnInitializedAsync()
    {
        try
        {
            await ClearLocalStorage();

            Players = await ManagePlayersLocalStorageService.GetCollection();

            var coachPlayers = await ManageCoachPlayersLocalStorageService.GetCollection();

            var totalQty = coachPlayers.Sum(i=>i.Qty);

            CoachService.RaiseEventOnCoachChanged(totalQty);
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

    private async Task ClearLocalStorage()
    {
        await ManagePlayersLocalStorageService.RemoveCollection();
        await ManageCoachPlayersLocalStorageService.RemoveCollection();
    }
}
