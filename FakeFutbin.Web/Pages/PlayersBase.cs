using FakeFutbin.Models.Dto;
using FakeFutbin.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace FakeFutbin.Web.Pages;

public class PlayersBase : ComponentBase
{
    [Inject]
    public IPlayerService PlayerService { get; set; }
    public IEnumerable<PlayerDto> Players { get; set; }
}
