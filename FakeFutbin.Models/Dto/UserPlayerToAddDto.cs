namespace FakeFutbin.Models.Dto;

public class UserPlayerToAddDto
{
    public int UserId { get; set; }
    public int PlayerId { get; set; }
    public int Qty { get; set; }
    public string Position { get; set; }
}
