using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeFutbin.Models.Dto;

public class UserPlayerQtyUpdateDto
{
    public int UserPlayerId { get; set; }
    public int Qty { get; set; }
}
