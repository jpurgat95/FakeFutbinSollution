using FakeFutbin.Models.Dto;
using FakeFutbin.Web.Services;
using FakeFutbin.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Microsoft.VisualBasic;

namespace FakeFutbin.Web.Pages;

public class UserBase : ComponentBase
{
    [Inject]
    public IJSRuntime Js { get; set; }
    [Inject]
    public IUserService UserService { get; set; }
    [Inject]
    public IManageUserPlayersLocalStorageService ManageUserPlayersLocalStorageService { get; set; }
    public List<UserPlayerDto> UserPlayers { get; set; }
    protected string TotalValue { get; set; }
    protected int TotalQuantity { get; set; }
    public string ErrorMessage { get; set; }    
    protected override async Task OnInitializedAsync()
    {
        try
        {
            UserPlayers = await ManageUserPlayersLocalStorageService.GetCollection();
            UserChanged();
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }

    protected async Task DeleteUserPlayer_Click(int id)
    {
        var userPlayerDto = await UserService.DeletePlayer(id);

        await RemoveUserPlayer(id);
        UserChanged();
    }
    protected async Task UpdateQtyUserPlayer_Click(int id, int qty)
    {
        try
        {
            if (qty > 0)
            {
                var updatePlayerDto = new UserPlayerQtyUpdateDto
                {
                    UserPlayerId = id,
                    Qty = qty
                };
                var returnedUpdatePlayerDto = await this.UserService.UpdateQty(updatePlayerDto);
                await UpdatePlayerTotalValue(returnedUpdatePlayerDto);
                UserChanged();
                await MakeUpdateQtyButtonVisible(id, false);
            }
            else
            {
                var player = this.UserPlayers.FirstOrDefault(x => x.Id == id);
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
    private async Task UpdatePlayerTotalValue(UserPlayerDto userPlayerDto)
    {
        var player = GetUserPlayer(userPlayerDto.Id);
        if (player != null)
        {
            player.TotalValue = userPlayerDto.MarketValue * userPlayerDto.Qty;
        }

        await ManageUserPlayersLocalStorageService.SaveColleciotn(UserPlayers);
    }
    private void CalculateScoutSummaryTotals()
    {
        SetTotalValue();
        SetTotalQuantity();
    }
    private void SetTotalValue()
    {
        TotalValue = this.UserPlayers.Sum(x=>x.TotalValue).ToString("C");
    }
    private void SetTotalQuantity()
    {
        TotalQuantity = this.UserPlayers.Sum(x => x.Qty);
    }
    private UserPlayerDto GetUserPlayer(int id)
    {
        return UserPlayers.FirstOrDefault(x => x.Id == id);
    }
    private async Task RemoveUserPlayer(int id)
    {
        var userPlayerDto = GetUserPlayer(id);
        UserPlayers.Remove(userPlayerDto);

        await ManageUserPlayersLocalStorageService.SaveColleciotn(UserPlayers);
    }

    private void UserChanged()
    {
        CalculateScoutSummaryTotals();
        UserService.RaiseEventOnUserChanged(TotalQuantity);
    }
}
