using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeFutbin.Models.Dto;

public class PlayerDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public int Raiting { get; set; }
    public string Position { get; set; }
    public string ImageURL { get; set; }
    public int MarketValue { get; set; }
    public int Qty { get; set; }
    public int NationalityId { get; set; }
    public string NationalityName { get; set; }
}
