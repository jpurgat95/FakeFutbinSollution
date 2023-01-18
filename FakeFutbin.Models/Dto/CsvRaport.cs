using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeFutbin.Models.Dto;

public class CsvRaport
{
    public DateTime DateTime { get; set; }
    public string Name { get; set; }
    public int Cash { get; set; }
    public string PlayerName { get; set; }
    public int PlayerAge { get; set; }
    public int PlayerRaiting { get; set; }
    public int MarketValue { get; set; }
    public int TotalValue { get; set; }
    public int Qty { get; set; }
    public string Position { get; set; }
    public string AvailablePositions { get; set; }
}
