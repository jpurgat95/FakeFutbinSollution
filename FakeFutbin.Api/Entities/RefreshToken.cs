﻿namespace FakeFutbin.Api.Entities;

public class RefreshToken
{
    public string Token { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public DateTime Expires { get; set; }
}
