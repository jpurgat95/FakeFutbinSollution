using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeFutbin.Models.Dto;

public class UserPlayerToAddDto
{
    public int UserId { get; set; }
    public int PlayerId { get; set; }
    public int Qty { get; set; }
}
