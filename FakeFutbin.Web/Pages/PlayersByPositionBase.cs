using FakeFutbin.Models.Dto;
using FakeFutbin.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace FakeFutbin.Web.Pages;

public class PlayersByPositionBase : ComponentBase
{
    [Parameter]
    public string Pos { get; set; }
    [Inject]
    public IPositionService PositionService { get; set; }
    [Inject]
    public IPlayerService PlayerService { get; set; }
    public IEnumerable<PlayerDto> Players { get; set; }
    public IEnumerable<PlayerDto> PlayerDtos { get; set; }
    public string ErrorMessage { get; set; }
    public IEnumerable<string> Positions { get; set; }
    protected override async Task OnInitializedAsync()
    {
        try
        {
            var players = await PlayerService.GetPlayers();
            Players = players;
            var positions = await PositionService.GetPositions();
            Positions = positions.Select(x => x.PlayerPosition).ToList();
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }

    }
}
