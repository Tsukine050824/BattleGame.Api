namespace BattleGame.Api.Models;

public sealed class Asset
{
    public int Id { get; set; }
    public string AssetName { get; set; } = default!;
    public string? AssetType { get; set; }
    public DateTime CreatedAt { get; set; }
}
