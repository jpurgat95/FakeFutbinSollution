using System.ComponentModel.DataAnnotations.Schema;

namespace FakeFutbin.Api.Entities;

public class Player
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public int Raiting { get; set; }
    public string Position { get; set; }
    public string ImageURL { get; set; }
    public int MarketValue { get; set; }
    public int Qty { get; set; }

    [ForeignKey("NationalityId")]
    public PlayerNationality PlayerNationality { get; set; }
}
