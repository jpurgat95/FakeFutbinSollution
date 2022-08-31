using FakeFutbin.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace FakeFutbin.Api.Data;

public class FakeFutbinDbContext: DbContext
{
    public FakeFutbinDbContext(DbContextOptions<FakeFutbinDbContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Scout> Scouts { get; set; }
    public DbSet<ScoutPlayer> ScoutPlayers { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<PlayerNationality> PlayerNationalities { get; set; }
    public DbSet<Coach> Coaches { get; set; }
}
