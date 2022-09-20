using FakeFutbin.Models.Dto;
using FakeFutbin.Web.Services;
using FakeFutbin.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Microsoft.VisualBasic;

namespace FakeFutbin.Web.Pages;

public class CoachBase : ComponentBase
{
    [Inject]
    public IJSRuntime Js { get; set; }
    [Inject]
    public ICoachService CoachService { get; set; }
    [Inject]
    public IManageCoachPlayersLocalStorageService ManageCoachPlayersLocalStorageService { get; set; }
    public List<CoachPlayerDto> CoachPlayers { get; set; }
    protected string TotalValue { get; set; }
    protected int TotalQuantity { get; set; }
    public string ErrorMessage { get; set; }    
    protected override async Task OnInitializedAsync()
    {
        try
        {
            CoachPlayers = await ManageCoachPlayersLocalStorageService.GetCollection();
            CoachChanged();
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }

    protected async Task DeleteCoachPlayer_Click(int id)
    {
        var scoutPlayerDto = await CoachService.DeletePlayer(id);

        await RemoveCoachPlayer(id);
        CoachChanged();
    }
    protected async Task UpdateQtyCoachPlayer_Click(int id, int qty)
    {
        try
        {
            if (qty > 0)
            {
                var updatePlayerDto = new CoachPlayerQtyUpdateDto
                {
                    CoachPlayerId = id,
                    Qty = qty
                };
                var returnedUpdatePlayerDto = await this.CoachService.UpdateQty(updatePlayerDto);
                await UpdatePlayerTotalValue(returnedUpdatePlayerDto);
                CoachChanged();
                await MakeUpdateQtyButtonVisible(id, false);
            }
            else
            {
                var player = this.CoachPlayers.FirstOrDefault(x => x.Id == id);
                if (player != null)
                {
                    player.Qty = 1;
                    player.TotalValue = player.MarketValue;

                }
            }
        }
            
        catch (Exception)
        {

            throw;
        }
    }
    protected async Task UpdateQty_Input(int id)
    {
        await MakeUpdateQtyButtonVisible(id, true);
    }
    private async Task MakeUpdateQtyButtonVisible(int id, bool visible)
    {
        await Js.InvokeVoidAsync("MakeUpdateQtyButtonVisible", id, visible);
    }
    private async Task UpdatePlayerTotalValue(CoachPlayerDto coachPlayerDto)
    {
        var player = GetCoachPlayer(coachPlayerDto.Id);
        if (player != null)
        {
            player.TotalValue = coachPlayerDto.MarketValue * coachPlayerDto.Qty;
        }

        await ManageCoachPlayersLocalStorageService.SaveColleciotn(CoachPlayers);
    }
    private void CalculateScoutSummaryTotals()
    {
        SetTotalValue();
        SetTotalQuantity();
    }
    private void SetTotalValue()
    {
        TotalValue = this.CoachPlayers.Sum(x=>x.TotalValue).ToString("C");
    }
    private void SetTotalQuantity()
    {
        TotalQuantity = this.CoachPlayers.Sum(x => x.Qty);
    }
    private CoachPlayerDto GetCoachPlayer(int id)
    {
        return CoachPlayers.FirstOrDefault(x => x.Id == id);
    }
    private async Task RemoveCoachPlayer(int id)
    {
        var coachPlayerDto = GetCoachPlayer(id);
        CoachPlayers.Remove(coachPlayerDto);

        await ManageCoachPlayersLocalStorageService.SaveColleciotn(CoachPlayers);
    }

    private void CoachChanged()
    {
        CalculateScoutSummaryTotals();
        CoachService.RaiseEventOnCoachChanged(TotalQuantity);
    }
}
