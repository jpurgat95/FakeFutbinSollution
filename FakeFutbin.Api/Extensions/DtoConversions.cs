using FakeFutbin.Api.Entities;
using FakeFutbin.Models.Dto;
using System.Net.Http.Headers;

namespace FakeFutbin.Api.Extensions;

public static class DtoConversions
{
    public static IEnumerable<PlayerDto> ConvertToDto(this IEnumerable<Player> players)
    {
        return (from player in players
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
                    NationalityId = player.PlayerNationality.Id,
                    NationalityName = player.PlayerNationality.Name
                }).ToList();
    }

    public static IEnumerable<PlayerNationalityDto> ConvertToDto(this IEnumerable<PlayerNationality> playerNationalities)
    {
        return (from playerNationality in playerNationalities
                select new PlayerNationalityDto
                {
                    Id = playerNationality.Id,
                    Name = playerNationality.Name,
                    ImageURL = playerNationality.ImageURL
                }).ToList();
    }

    public static PlayerDto ConvertToDto(this Player player)
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
            NationalityId = player.PlayerNationality.Id,
            NationalityName = player.PlayerNationality.Name
        };
    }

    public static IEnumerable<UserPlayerDto> ConvertToDto(this IEnumerable<UserPlayer> coachPlayers,
                                                           IEnumerable<Player> players)
    {
        return (from coachPlayer in coachPlayers
                join player in players
                on coachPlayer.PlayerId equals player.Id
                select new UserPlayerDto
                {
                    Id = coachPlayer.Id,
                    PlayerId = coachPlayer.PlayerId,
                    PlayerName = player.Name,
                    PlayerAge = player.Age,
                    PlayerRaiting = player.Raiting,
                    PlayerImageURL = player.ImageURL,
                    MarketValue = player.MarketValue,
                    UserId = coachPlayer.UserId,
                    Qty = coachPlayer.Qty,
                    TotalValue = player.MarketValue * coachPlayer.Qty,
                }).ToList();
    }

    public static UserPlayerDto ConvertToDto(this UserPlayer coachPlayer,
                                             Player player)
    {
        return new UserPlayerDto
        {
            Id = coachPlayer.Id,
            PlayerId = coachPlayer.PlayerId,
            PlayerName = player.Name,
            PlayerAge = player.Age,
            PlayerRaiting = player.Raiting,
            PlayerImageURL = player.ImageURL,
            MarketValue = player.MarketValue,
            UserId = coachPlayer.UserId,
            Qty = coachPlayer.Qty,
            TotalValue = player.MarketValue * coachPlayer.Qty,
        };
    }
}
