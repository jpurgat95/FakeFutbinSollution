namespace FakeFutbin.Models.Dto;

public class UserPlayerDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int PlayerId { get; set; }
    public string PlayerName { get; set; }
    public int PlayerAge { get; set; }
    public int PlayerRaiting { get; set; }
    public string PlayerImageURL { get; set; }
    public int MarketValue { get; set; }
    public int TotalValue { get; set; }
    public int Qty { get; set; }
    public string Position { get; set; }
    public string AvailablePositions { get; set; }
}
