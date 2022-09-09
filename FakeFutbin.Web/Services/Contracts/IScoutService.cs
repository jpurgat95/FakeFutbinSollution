﻿using FakeFutbin.Models.Dto;

namespace FakeFutbin.Web.Services.Contracts;

public interface IScoutService
{
    Task<List<ScoutPlayerDto>> GetPlayers(int coachId);
    Task<ScoutPlayerDto> AddPlayer(ScoutPlayerToAddDto scoutPlayerToAddDto);
    Task<ScoutPlayerDto> DeletePlayer(int id);
}