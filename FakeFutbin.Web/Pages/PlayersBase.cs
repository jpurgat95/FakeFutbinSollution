﻿namespace FakeFutbin.Web.Pages;

public class PlayersBase : ComponentBase
{
    [Inject]
    public IUserService UserService { get; set; }
    [Inject]
    public IUserIdService UserIdService { get; set; }
    [Inject]
    public IManagePlayersLocalStorageService ManagePlayersLocalStorageService { get; set; }
    [Inject]
    public IManageUserPlayersLocalStorageService ManageUserPlayersLocalStorageService { get; set; }
    [Inject]
    public IManageUserLocalStorageService ManageUserLocalStorageService { get; set; }
    public IEnumerable<PlayerDto> Players { get; set; }
    [Inject]
    public NavigationManager NavigationManager { get; set; }
    public string ErrorMessage { get; set; }
    public string SearchName = "";
    [Parameter]
    public IEnumerable<PlayerDto> SortedPlayers { get; set; }
    protected override async Task OnInitializedAsync()
    {
        try
        {
            await ClearLocalStorage();
            Players = await ManagePlayersLocalStorageService.GetCollection();

            var userPlayers = await ManageUserPlayersLocalStorageService.GetCollection();
            var users = await ManageUserLocalStorageService.GetCollection();
            var totalQty = userPlayers.Sum(i => i.Qty);
            var userId = await UserIdService.GetUserId();
            var wallet = users.FirstOrDefault(x => x.Id == userId).Wallet;

            UserService.RaiseEventOnUserChanged(totalQty);
            UserService.RaiseEventOnWalletChanged(wallet);
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }
    public List<PlayerDto> FilteredPlayers => Players.Where(x => x.Name.ToLower().Contains(SearchName.ToLower())).ToList();
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
        await ManageUserPlayersLocalStorageService.RemoveCollection();
        await ManageUserLocalStorageService.RemoveCollection();
   }
    public async Task SortingPlayersAscending()
    {
        try
        {
            SortedPlayers = Players.OrderBy(x => x.MarketValue).ToList();
            SearchName = String.Empty;
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }
    public async Task SortingPlayersDescending()
    {
        try
        {
            SortedPlayers = Players.OrderByDescending(x => x.MarketValue).ToList();
            SearchName = String.Empty;
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }
    public void SortingPlayersByNationaity()
    {
        try
        {
            SortedPlayers = null;
            SearchName = String.Empty;
        }
        catch (Exception ex)
        {

            ErrorMessage = ex.Message;
        }
    }
}
