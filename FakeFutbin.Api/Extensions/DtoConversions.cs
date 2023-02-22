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

    public static IEnumerable<UserPlayerDto> ConvertToDto(this IEnumerable<UserPlayer> userPlayers,
                                                           IEnumerable<Player> players)
    {
        return (from userPlayer in userPlayers
                join player in players
                on userPlayer.PlayerId equals player.Id
                select new UserPlayerDto
                {
                    Id = userPlayer.Id,
                    PlayerId = userPlayer.PlayerId,
                    PlayerName = player.Name,
                    PlayerAge = player.Age,
                    PlayerRaiting = player.Raiting,
                    PlayerImageURL = player.ImageURL,
                    MarketValue = player.MarketValue,
                    UserId = userPlayer.UserId,
                    Qty = userPlayer.Qty,
                    TotalValue = player.MarketValue * userPlayer.Qty,
                    Position = userPlayer.Position,
                    AvailablePositions = player.Position,
                }).ToList();
    }

    public static UserPlayerDto ConvertToDto(this UserPlayer userPlayer,
                                             Player player)
    {
        return new UserPlayerDto
        {
            Id = userPlayer.Id,
            PlayerId = userPlayer.PlayerId,
            PlayerName = player.Name,
            PlayerAge = player.Age,
            PlayerRaiting = player.Raiting,
            PlayerImageURL = player.ImageURL,
            MarketValue = player.MarketValue,
            UserId = userPlayer.UserId,
            Qty = userPlayer.Qty,
            TotalValue = player.MarketValue * userPlayer.Qty,
        };
    }
    public static UserWalletDto ConvertToDto(this User user)
    {
        return new UserWalletDto
        {
            Id = user.Id,
            Wallet = user.Wallet,
            Username = user.Username,
        };
    }
    public static IEnumerable<UserWalletDto> ConvertToDto(this IEnumerable<User> users)
    {
        return(from user in users
               select new UserWalletDto
               {
                   Id = user.Id,
                   Wallet = user.Wallet,
                   Username= user.Username,
               }).ToList();
    }
    public static UserPlayerPositionDto ConvertToDto(this UserPlayer userPlayer)
    {
        return new UserPlayerPositionDto
        {
            Id = userPlayer.Id,
            Position = userPlayer.Position,
        };
    }
    public static IEnumerable<PositionDto> ConvertToDto(this IEnumerable<Position> positions)
    {
        return (from position in positions
                select new PositionDto
                {
                    Id = position.Id,
                    PlayerPosition = position.PlayerPosition,
                }).ToList();
    }
}
