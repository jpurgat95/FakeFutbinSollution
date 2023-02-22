namespace FakeFutbin.Web.Pages;

public class PlayersByNationalityBase : ComponentBase
{
    [Parameter]
    public int NationalityId {get; set;}
    [Inject]
    public IPlayerService PlayerService { get; set; }
    [Inject]
    public IManagePlayersLocalStorageService ManagePlayersLocalStorageService { get; set; }
    public IEnumerable<PlayerDto> Players { get; set; }
    public string NationalityName { get; set; }
    public string ErrorMessage { get; set; }
    protected override async Task OnParametersSetAsync()
    {
        try
        {
            Players = await GetPlayerCollectionByNationalityId(NationalityId);
            if (Players != null && Players.Count()>0)
            {
                var playerDto = Players.FirstOrDefault(x=> x.NationalityId == NationalityId);
                if (playerDto != null)
                {
                    NationalityName = playerDto.NationalityName;
                }
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }

    private async Task<IEnumerable<PlayerDto>> GetPlayerCollectionByNationalityId(int nationalityId)
    {
        var playerCollection = await ManagePlayersLocalStorageService.GetCollection();

        if(playerCollection != null)
        {
            return playerCollection.Where(x => x.NationalityId == nationalityId);
        }
        else
        {
            return await PlayerService.GetPlayersByNationality(nationalityId);
        }
    }
}
