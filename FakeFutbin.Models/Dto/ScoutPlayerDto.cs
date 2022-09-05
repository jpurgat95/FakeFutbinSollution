using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeFutbin.Models.Dto;

public class ScoutPlayerDto
{
    public int Id { get; set; }
    public int ScoutId { get; set; }
    public int PlayerId { get; set; }
    public string PlayertName { get; set; }
    public string PlayerDescription { get; set; }
    public string PlayerImageURL { get; set; }
    public decimal Price { get; set; }
    public decimal TotalPrice { get; set; }
    public int Qty { get; set; }
}
