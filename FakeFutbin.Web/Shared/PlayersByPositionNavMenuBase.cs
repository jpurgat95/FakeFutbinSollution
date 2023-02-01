using FakeFutbin.Models.Dto;
using FakeFutbin.Web.Pages;
using FakeFutbin.Web.Services;
using FakeFutbin.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace FakeFutbin.Web.Shared;

public class PlayersByPositionNavMenuBase : ComponentBase
{
    [Inject]
    public IPositionService PositionService { get; set; }
    public IEnumerable<string> Positions { get; set; }
    public IEnumerable<PositionDto> PositionDtos { get; set; }
    public string ErrorMessage { get; set; }
    public int[] PosIds { get; set; }
    protected override async Task OnInitializedAsync()
    {
        try
        {
            var positions = await PositionService.GetPositions();
            PositionDtos = positions;
            Positions = positions.Select(x => x.PlayerPosition).ToList();
            PosIds = positions.Select(x => x.Id).ToArray();
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }
}
