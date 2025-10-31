using BattleGame.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BattleGame.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Player> Players => Set<Player>();
    public DbSet<Asset> Assets => Set<Asset>();
    public DbSet<PlayerAsset> PlayerAssets => Set<PlayerAsset>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<Player>(e =>
        {
            e.ToTable("Player");
            e.HasIndex(x => x.PlayerName).IsUnique();
            e.Property(x => x.PlayerName).HasMaxLength(100).IsRequired();
            e.Property(x => x.FullName).HasMaxLength(150).IsRequired();
        });

        b.Entity<Asset>(e =>
        {
            e.ToTable("Asset");
            e.Property(x => x.AssetName).HasMaxLength(120).IsRequired();
            e.Property(x => x.Description).HasMaxLength(400);
        });

        b.Entity<PlayerAsset>(e =>
        {
            e.ToTable("PlayerAsset");
            e.HasKey(x => new { x.PlayerId, x.AssetId });
            e.HasOne(x => x.Player).WithMany(p => p.PlayerAssets).HasForeignKey(x => x.PlayerId);
            e.HasOne(x => x.Asset).WithMany(a => a.PlayerAssets).HasForeignKey(x => x.AssetId);
        });

        base.OnModelCreating(b);
    }
}
