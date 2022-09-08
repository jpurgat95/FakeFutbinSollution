using FakeFutbin.Api.Entities;
using FakeFutbin.Models.Dto;
using System.Net.Http.Headers;

namespace FakeFutbin.Api.Extensions;

public static class DtoConversions
{
    public static IEnumerable<PlayerDto> ConvertToDto (this IEnumerable<Player> players,
                                                       IEnumerable<PlayerNationality> playerNationalities)
    {
        return (from player in players
                join playerNationality in playerNationalities
                on player.NationalityId equals playerNationality.Id
                select new PlayerDto
                {
                    Id = player.Id,
                    Name = player.Name,
                    Age = player.Age,
                    Raiting = player.Raiting,
                    Position = player.Position,
                    ImageURL = player.ImageURL,
                    MarketValue = player.MarketValue,
                    Qty = player.Qty,
                    NationalityId = player.NationalityId,
                    NationalityName = player.PlayerNationality.Name
                }).ToList();
    }

    public static IEnumerable<PlayerNationalityDto> ConvertToDto (this IEnumerable<PlayerNationality> playerNationalities)
    {
        return (from playerNationality in playerNationalities
                select new PlayerNationalityDto
                {
                    Id = playerNationality.Id,
                    Name = playerNationality.Name,
                    ImageURL = playerNationality.ImageURL
                }).ToList();
    }

    public static PlayerDto ConvertToDto(this Player player,
                                        PlayerNationality playerNationality)
    {
        return new PlayerDto
        {
            Id = player.Id,
            Name = player.Name,
            Age = player.Age,
            Raiting = player.Raiting,
            Position = player.Position,
            ImageURL = player.ImageURL,
            MarketValue = player.MarketValue,
            Qty = player.Qty,
            NationalityId = player.NationalityId,
            NationalityName = playerNationality.Name
        };
    }

    public static IEnumerable<ScoutPlayerDto> ConvertToDto(this IEnumerable<ScoutPlayer> scoutPlayers,
                                                           IEnumerable<Player> players)
    {
        return (from scoutPlayer in scoutPlayers
                join player in players
                on scoutPlayer.PlayerId equals player.Id
                select new ScoutPlayerDto
                {
                    Id = scoutPlayer.Id,
                    PlayerId = scoutPlayer.PlayerId,
                    PlayerName = player.Name,
                    PlayerAge = player.Age,
                    PlayerRaiting = player.Raiting,
                    PlayerImageURL = player.ImageURL,
                    MarketValue = player.MarketValue,
                    ScoutId = scoutPlayer.ScoutId,
                    Qty = scoutPlayer.Qty,
                    TotalValue = player.MarketValue * scoutPlayer.Qty,
                }).ToList();
    } 

    public static ScoutPlayerDto ConvertToDto(this ScoutPlayer scoutPlayer,
                                             Player player)
    {
        return new ScoutPlayerDto
        {
            Id = scoutPlayer.Id,
            PlayerId = scoutPlayer.PlayerId,
            PlayerName = player.Name,
            PlayerAge = player.Age,
            PlayerRaiting = player.Raiting,
            PlayerImageURL = player.ImageURL,
            MarketValue = player.MarketValue,
            ScoutId = scoutPlayer.ScoutId,
            Qty = scoutPlayer.Qty,
            TotalValue = player.MarketValue * scoutPlayer.Qty,
        };
    }

}
