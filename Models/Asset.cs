namespace BattleGame.Api.Models;

public class Asset
{
    public int Id { get; set; }
    public string AssetName { get; set; } = default!; // ví dụ: "Hero 1", "Sword +1"
    public string? Description { get; set; }

    public ICollection<PlayerAsset> PlayerAssets { get; set; } = new List<PlayerAsset>();
}
