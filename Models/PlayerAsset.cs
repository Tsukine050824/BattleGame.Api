namespace BattleGame.Api.Models;

public class PlayerAsset
{
    public int PlayerId { get; set; }
    public Player Player { get; set; } = default!;
    public int AssetId { get; set; }
    public Asset Asset { get; set; } = default!;
    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
}
