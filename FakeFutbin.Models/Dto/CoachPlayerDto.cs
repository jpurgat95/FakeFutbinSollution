using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeFutbin.Models.Dto;

public class CoachPlayerDto
{
    public int Id { get; set; }
    public int CoachId { get; set; }
    public int PlayerId { get; set; }
    public string PlayerName { get; set; }
    public int PlayerAge { get; set; }
    public int PlayerRaiting { get; set; }
    public string PlayerImageURL { get; set; }
    public int MarketValue { get; set; }
    public int TotalValue { get; set; }
    public int Qty { get; set; }
}
