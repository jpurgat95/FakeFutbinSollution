namespace FakeFutbin.Web.Shared;

public class PlayerNationalitiesNavMenuBase:ComponentBase 
{
    [Inject]
    public IPlayerService PlayerService { get; set; }
    public IEnumerable<PlayerNationalityDto> PlayerNationalityDtos { get; set; }
    public string ErrorMessage { get; set; }
    protected override async Task OnInitializedAsync()
    {
        try
        {
            PlayerNationalityDtos = await PlayerService.GetPlayerNationalities();
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }

}
