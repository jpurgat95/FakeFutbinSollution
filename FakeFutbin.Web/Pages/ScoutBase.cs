using FakeFutbin.Models.Dto;
using FakeFutbin.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Microsoft.VisualBasic;

namespace FakeFutbin.Web.Pages;

public class ScoutBase : ComponentBase
{
    [Inject]
    public IJSRuntime Js { get; set; }
    [Inject]
    public IScoutService ScoutService { get; set; }
    public List<ScoutPlayerDto> ScoutPlayers { get; set; }
    protected string TotalValue { get; set; }
    protected int TotalQuantity { get; set; }
    public string ErrorMessage { get; set; }    
    protected override async Task OnInitializedAsync()
    {
        try
        {
            ScoutPlayers = await ScoutService.GetPlayers(HardCoded.CoachId);
            CalculateScoutSummaryTotals();
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
        CalculateScoutSummaryTotals();
    }
    protected async Task UpdateQtyScoutPlayer_Click(int id, int qty)
    {
        try
        {
            if (qty > 0)
            {
                var updatePlayerDto = new ScoutPlayerQtyUpdateDto
                {
                    ScoutPlayerId = id,
                    Qty = qty
                };
                var returnedUpdatePlayerDto = await this.ScoutService.UpdateQty(updatePlayerDto);
                UpdatePlayerTotalValue(returnedUpdatePlayerDto);
                CalculateScoutSummaryTotals();
                await MakeUpdateQtyButtonVisible(id, false);
            }
            else
            {
                var player = this.ScoutPlayers.FirstOrDefault(x => x.Id == id);
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
    private void UpdatePlayerTotalValue(ScoutPlayerDto scoutPlayerDto)
    {
        var player = GetScoutPlayer(scoutPlayerDto.Id);
        if (player != null)
        {
            player.TotalValue = scoutPlayerDto.MarketValue * scoutPlayerDto.Qty;
        }
    }
    private void CalculateScoutSummaryTotals()
    {
        SetTotalValue();
        SetTotalQuantity();
    }
    private void SetTotalValue()
    {
        TotalValue = this.ScoutPlayers.Sum(x=>x.TotalValue).ToString();
    }
    private void SetTotalQuantity()
    {
        TotalQuantity = this.ScoutPlayers.Sum(x => x.Qty);
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
