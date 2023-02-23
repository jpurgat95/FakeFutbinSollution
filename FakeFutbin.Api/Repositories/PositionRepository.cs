namespace FakeFutbin.Api.Repositories;

public class PositionRepository : IPositionRepository
{
    private readonly FakeFutbinDbContext _fakeFutbinDbContext;

    public PositionRepository(FakeFutbinDbContext fakeFutbinDbContext)
    {
        _fakeFutbinDbContext = fakeFutbinDbContext;
    }
    public async Task<IEnumerable<Position>> GetPositions()
    {
        var positions = await _fakeFutbinDbContext.Positions.ToListAsync();
        return positions;
    }
    public async Task<UserPlayer> UpdatePosition(int id, UserPlayerPositionUpdateDto userPlayerPositionUpdateDto)
    {
        var userPlayer = await _fakeFutbinDbContext.UserPlayers.SingleOrDefaultAsync(p => p.PlayerId == id);
        if (userPlayer != null)
        {
            userPlayer.Position = userPlayerPositionUpdateDto.Position;
            await _fakeFutbinDbContext.SaveChangesAsync();
            return userPlayer;
        }
        return null;
    }
}