﻿namespace FakeFutbin.Web.Services.Contracts;

public interface IPlayerService
{
    Task<IEnumerable<PlayerDto>> GetPlayers();
    Task<PlayerDto> GetPlayer(int id);
    Task<IEnumerable<PlayerNationalityDto>> GetPlayerNationalities();
    Task<IEnumerable<PlayerDto>> GetPlayersByNationality (int nationalityId);
}
