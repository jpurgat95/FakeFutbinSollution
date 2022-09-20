namespace FakeFutbin.Api.Entities;

public class CoachPlayer
{
    public int Id { get; set; }
    public int CoachId { get; set; }
    public int PlayerId { get; set; }
    public int Qty { get; set; }
}
