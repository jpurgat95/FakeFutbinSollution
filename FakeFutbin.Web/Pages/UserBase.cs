using CsvHelper.Configuration;
using Microsoft.JSInterop;
using System.Globalization;

namespace FakeFutbin.Web.Pages;

public class UserBase : ComponentBase
{
    [Inject]
    public IJSRuntime Js { get; set; }
    [Inject]
    public IUserService UserService { get; set; }
    [Inject]
    public IPositionService PositionService { get; set; }
    [Inject]
    public IManageUserPlayersLocalStorageService ManageUserPlayersLocalStorageService { get; set; }
    [Inject]
    public IUserIdService UserIdService { get; set; }
    [Inject]
    public IManageUserLocalStorageService ManageUserLocalStorageService { get; set; }
    [Inject]
    public IToastService ToastService { get; set; }
    public List<UserWalletDto> UserDtos { get; set; }
    public List<UserPlayerDto> UserPlayers { get; set; }
    protected string TotalValue { get; set; }
    protected int TotalQuantity { get; set; }
    protected int WalletValue { get; set; }
    public string ErrorMessage { get; set; }
    public string Position { get; set; }
    public string Username { get; set; }
    protected override async Task OnInitializedAsync()
    {
        try
        {
            await ManageUserPlayersLocalStorageService.RemoveCollection();
            UserPlayers = await ManageUserPlayersLocalStorageService.GetCollection();
            UserDtos = await ManageUserLocalStorageService.GetCollection();
            UserChanged();
            var userId = await UserIdService.GetUserId();
            WalletValue = UserDtos.FirstOrDefault(x => x.Id == userId).Wallet;
            var wallet = WalletValue;
            UserService.RaiseEventOnWalletChanged(wallet);
            Username = UserDtos.FirstOrDefault(x => x.Id == userId).Username;
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }

    protected async Task DeleteUserPlayer_Click(int id)
    {
        var userPlayerDto = await UserService.DeletePlayer(id);
        var userId = await UserIdService.GetUserId();
        var user = UserDtos.FirstOrDefault(x => x.Id == userId);
        var userPlayers = UserPlayers.Where(x => x.UserId == userId).ToList();
        var userPlayer = userPlayers.FirstOrDefault(x => x.Id == id);

        var walletChanged = new UserWalletUpdateDto
        {
            Wallet = user.Wallet + userPlayer.TotalValue
        };
       await UserService.UpdateWallet(userId, walletChanged);

        var userChanged = new UserWalletDto
        {
            Id = userId,
            Wallet = user.Wallet + userPlayer.TotalValue
        };
        UpdateUserWalletValue(userChanged);
        UserChanged();
        await RemoveUserPlayer(id);
        UserChanged();
    }
    protected async Task UpdateQtyUserPlayer_Click(int id, int qty)
    {
        try
        {
            if (qty > 0)
            {
                var userId = await UserIdService.GetUserId();
                var user = UserDtos.FirstOrDefault(x => x.Id == userId);
                var userPlayersCollection = await ManageUserPlayersLocalStorageService.GetCollection();
                var userPlayer = userPlayersCollection.FirstOrDefault(x => x.Id == id);
                var userPlayerQty = userPlayer.Qty;
                var usersCollection = await ManageUserLocalStorageService.GetCollection();
                var userWallet = usersCollection.FirstOrDefault(x => x.Id == userId).Wallet;

                if (userPlayerQty > qty)
                {
                    var userChanged = new UserWalletDto
                    {
                        Id = userId,
                        Wallet = user.Wallet + userPlayer.MarketValue * (userPlayerQty - qty),
                    };
                    var walletChanged = new UserWalletUpdateDto
                    {
                        Wallet = user.Wallet + userPlayer.MarketValue * (userPlayerQty - qty),
                    };
                    UpdateUserWalletValue(userChanged);
                    await UserService.UpdateWallet(userId, walletChanged);

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
                else if (userWallet >= userPlayer.MarketValue * (qty - userPlayerQty))
                {
                    var userChanged = new UserWalletDto
                    {
                        Id = userId,
                        Wallet = user.Wallet - userPlayer.MarketValue * (qty - userPlayerQty),
                    };
                    var walletChanged = new UserWalletUpdateDto
                    {
                        Wallet = user.Wallet - userPlayer.MarketValue * (qty - userPlayerQty),
                    };
                    UpdateUserWalletValue(userChanged);

                    await UserService.UpdateWallet(userId, walletChanged);
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
                    var currentUserPlayerQty =  userPlayersCollection.FirstOrDefault(x => x.Id == id).Qty;
                    player.Qty = currentUserPlayerQty;
                    await MakeUpdateQtyButtonVisible(id, false);
                    ToastService.ShowWarning("", "You don't have enough money!");
                }
            }
            else
            {
                var userPlayersCollection = await ManageUserPlayersLocalStorageService.GetCollection();
                var player = this.UserPlayers.FirstOrDefault(x => x.Id == id);
                var currentUserPlayerQty = userPlayersCollection.FirstOrDefault(x => x.Id == id).Qty;

                if (player != null)
                {
                    player.Qty = currentUserPlayerQty;
                    player.TotalValue = player.TotalValue;
                    await MakeUpdateQtyButtonVisible(id, false);
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
    private async void UpdateUserWalletValue(UserWalletDto userDto2)
    {
        var user = GetUser(userDto2.Id);
        if (user != null)
        {
            user.Wallet = userDto2.Wallet;
        }

        await ManageUserLocalStorageService.SaveColleciotn(UserDtos);
    }

    private async Task<int> CalculateScoutSummaryTotals()
    {
        SetTotalValue();
        SetTotalQuantity();
        await SetWalletValue();
        return 0;
    }
    private void SetTotalValue()
    {
        TotalValue = this.UserPlayers.Sum(x=>x.TotalValue).ToString("C");
    }
    private void SetTotalQuantity()
    {
        TotalQuantity = this.UserPlayers.Sum(x => x.Qty);
    }
    private async Task<int> SetWalletValue()
    {
        var userId = await UserIdService.GetUserId();
        var user = this.UserDtos.FirstOrDefault(x => x.Id == userId);
        WalletValue = user.Wallet;
        await ManageUserLocalStorageService.SaveColleciotn(UserDtos);
        return 0;
    }
    private UserPlayerDto GetUserPlayer(int id)
    {
        return UserPlayers.FirstOrDefault(x => x.Id == id);
    }
    private UserWalletDto GetUser(int id)
    {

        return UserDtos.FirstOrDefault(x => x.Id == id);
    }

    private async Task RemoveUserPlayer(int id)
    {
        var userPlayerDto = GetUserPlayer(id);
        UserPlayers.Remove(userPlayerDto);

        await ManageUserPlayersLocalStorageService.SaveColleciotn(UserPlayers);
    }

    private async void UserChanged()
    {
        await CalculateScoutSummaryTotals();
        UserService.RaiseEventOnUserChanged(TotalQuantity);
        UserService.RaiseEventOnWalletChanged(WalletValue);
    }
    public void OnChange(object value)
    {
        var str = value is IEnumerable<object> ? string.Join(",", (IEnumerable<object>)value) : value;
        
        Console.WriteLine($"Value changed to {str}");

        Position = str.ToString();
    }
    public async void OnClickPositionChange(int id)
    {
        var pos = Position;
        PositionService.UpdatePosition(id, new UserPlayerPositionUpdateDto
        {
            Position = pos,
        });

        
        var userPlayersCollection = await ManageUserPlayersLocalStorageService.GetCollection();
        var player = this.UserPlayers.FirstOrDefault(x => x.PlayerId == id);

        if (player != null)
        {
            player.Position = pos;
        }
        Position = null;
    }
    public sealed class CsvRaportMap : ClassMap<CsvRaport>
    {
        public CsvRaportMap()
        {
            Map(m => m.DateTime).Name("Date & Time");
            Map(m => m.Name).Name("Username");
            Map(m => m.Cash).Name("User's Coins");
            Map(m => m.PlayerName).Name("Player Name");
            Map(m => m.PlayerAge).Name("Player Age");
            Map(m => m.PlayerRaiting).Name("Player Raiting");
            Map(m => m.MarketValue).Name("Player Value");
            Map(m => m.TotalValue).Name("Player Total Value");
            Map(m => m.Qty).Name("Player Quantity");
            Map(m => m.Position).Name("Player Current Position");
            Map(m => m.AvailablePositions).Name("Player Available Posotions");
        }
    }
    public byte[] WriteCsv(List<CsvRaport> records)
    {
        using (var memoryStream = new MemoryStream())
        using (var streamWriter = new StreamWriter(memoryStream))
        using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture, true))
        {
            csvWriter.Context.RegisterClassMap<CsvRaportMap>();
            csvWriter.WriteRecords(records);
            streamWriter.Flush();
            return memoryStream.ToArray();
        }
    }
    public Stream ExportPayments()
    {
        var records = (from player in UserPlayers
                       select new CsvRaport
                       {
                           Name = Username,
                           Cash = WalletValue,
                           DateTime = DateTime.Now,
                           PlayerName = player.PlayerName,
                           PlayerAge = player.PlayerAge,
                           PlayerRaiting = player.PlayerRaiting,
                           MarketValue = player.MarketValue,
                           TotalValue = player.TotalValue,
                           Qty = player.Qty,
                           Position = player.Position,
                           AvailablePositions = player.AvailablePositions,
                       }).ToList();

        var result = WriteCsv(records);
        var memoryStream = new MemoryStream(result);
        return memoryStream;
    }

    public async Task DownloadFileFromStream()
    {
        var fileStream = ExportPayments();
        var fileName = "Raport.csv";

        using var streamRef = new DotNetStreamReference(stream: fileStream);

        await Js.InvokeVoidAsync("downloadFileFromStream", fileName, streamRef);
    }
}