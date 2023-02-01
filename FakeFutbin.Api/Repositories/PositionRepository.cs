using Microsoft.EntityFrameworkCore;

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
}
