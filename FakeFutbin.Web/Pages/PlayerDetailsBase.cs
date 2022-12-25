using Blazored.Toast.Services;
using FakeFutbin.Models.Dto;
using FakeFutbin.Web.Services;
using FakeFutbin.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.VisualBasic;

namespace FakeFutbin.Web.Pages;
public class PlayerDetailsBase : ComponentBase
{
    [Parameter]
    public int Id { get; set; }
    [Inject]
    public IPlayerService PlayerService { get; set; }
    [Inject]
    public IUserService UserService { get; set; }
    [Inject]
    public IManagePlayersLocalStorageService ManagePlayersLocalStorageService { get; set; }
    [Inject]
    public IManageUserPlayersLocalStorageService ManageUserPlayersLocalStorageService { get; set; }
    [Inject]
    public IManageUserLocalStorageService ManageUserLocalStorageService { get; set; }
    [Inject]
    public NavigationManager NavigationManager { get; set; }
    [Inject]
    public  IUserIdService UserIdService{ get; set; }
    [Inject]
    public IToastService ToastService { get; set; }
    public PlayerDto Player { get; set; }
    public List<UserDto2> UserDtos { get; set; }
    public string ErrorMessage { get; set; }
    public List<UserPlayerDto> UserPlayers { get; set; }
    public int UserPlayerQty { get; set; }
    public int UserPlayerId { get; set; }
    public UserPlayerToAddDto UserPlayerToAddDto { get; set; }
    public IEnumerable<string> PlayerPositions { get; set; }
    protected override async Task OnInitializedAsync()
    {
        try
        {
            UserPlayers = await ManageUserPlayersLocalStorageService.GetCollection();
            UserDtos = await ManageUserLocalStorageService.GetCollection();
            
            Player = await GetPlayerById(Id);
            UserPlayerQty = await GetPlayerQty();

            var playerPositions = Player.Position.ToString();
            PlayerPositions = !string.IsNullOrEmpty(playerPositions)
            ? playerPositions.Split(',') : Enumerable.Empty<string>();

            var userPlayerQty = UserPlayerQty;
            var userId = await UserIdService.GetUserId();
            var playerId = Player.Id;

            var userPlayers = UserPlayers.Where(p => p.UserId == userId).ToList();
            var userPlayer = userPlayers.FirstOrDefault(p => p.PlayerId == playerId);
            if (userPlayer != null)
            {               
                UserPlayerId = userPlayer.Id;
            }
            else
            {
                UserPlayerId = 0;
            }


            UserPlayerToAddDto = new UserPlayerToAddDto
            {
                UserId = userId,
                PlayerId = playerId,
                Qty = userPlayerQty,
            };
        }
        catch (Exception ex)
        {

            ErrorMessage = ex.Message;
        }
    }

    protected async Task AddToUser_Click(UserPlayerToAddDto userPlayerToAddDto)
    {
        try
        {
            await ManageUserLocalStorageService.RemoveCollection();
            var userId = await UserIdService.GetUserId();
            var user = UserDtos.FirstOrDefault(x => x.Id == userId);
            var userWallet = UserDtos.FirstOrDefault(x => x.Id == userId).Wallet;
            var userPlayer = Player;

            if (userWallet >= userPlayer.MarketValue)
            {
                var walletChanged = new UserWalletUpdateDto
                {
                    Wallet = user.Wallet - userPlayer.MarketValue,
                };
                await UserService.UpdateWallet(userId, walletChanged);

                var userPlayerQty = UserPlayerQty;
                if(userPlayerQty == 1)
                {
                    var userPlayerDto = await UserService.AddPlayer(userPlayerToAddDto);
                    if (userPlayerDto != null)
                    {
                        UserPlayers.Add(userPlayerDto);
                        await ManageUserPlayersLocalStorageService.SaveColleciotn(UserPlayers);
                        NavigationManager.NavigateTo("/User");
                    }
                }
                else
                {                                    
                    var userPlayerId = UserPlayerId;
                    await UserService.DeletePlayer(userPlayerId);

                    var userPlayerToAdd = UserPlayerToAddDto;
                    var addedPlayer = await UserService.AddPlayer(userPlayerToAdd);
                    UserPlayers.Add(addedPlayer);
                    await ManageUserPlayersLocalStorageService.SaveColleciotn(UserPlayers);
                    NavigationManager.NavigateTo("/User");
                }

            }
            else
            {               
                await ManageUserLocalStorageService.SaveColleciotn(UserDtos);
                ToastService.ShowWarning("", "You don't have enough money!");
            }

            
        }
        catch (Exception)
        {

            //Log Exception
        }
    }

    private async Task<PlayerDto> GetPlayerById(int id)
    {
        var playerDtos = await ManagePlayersLocalStorageService.GetCollection();

        if(playerDtos != null)
        {
            return playerDtos.SingleOrDefault(p => p.Id == id);
        }
        return null;
    }
    public async Task<int> GetPlayerQty()
    {
        var userId = await UserIdService.GetUserId();
        var playerId = Player.Id;
        var userPlayers = UserPlayers.Where(p => p.UserId == userId).ToList();
        var userPlayer = userPlayers.FirstOrDefault(p => p.PlayerId == playerId);
        if (userPlayer != null)
        {
            var userPlayerQty = userPlayer.Qty;
            var increasedQty = userPlayerQty + 1;
            return increasedQty;
        }
        return 1;
    }
    public void OnChange(object value)
    {
        var str = value is IEnumerable<object> ? string.Join(", ", (IEnumerable<object>)value) : value;

        Console.WriteLine($"Value changed to {str}");
    }
}