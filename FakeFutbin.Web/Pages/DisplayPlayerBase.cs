namespace FakeFutbin.Web.Pages;

public class DisplayPlayerBase : ComponentBase
{
    [Parameter]
    public IEnumerable<PlayerDto> Players { get; set; }
}
