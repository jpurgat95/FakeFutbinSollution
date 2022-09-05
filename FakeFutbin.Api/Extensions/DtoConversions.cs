using FakeFutbin.Api.Entities;
using FakeFutbin.Models.Dto;

namespace FakeFutbin.Api.Extensions;

public static class DtoConversions
{
    public static IEnumerable<PlayerDto> ConvertToDto (this IEnumerable<Player> players)
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

}
