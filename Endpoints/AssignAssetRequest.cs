namespace BattleGame.Api.Models;

public sealed class AssignAssetRequest
{
    public int PlayerId { get; set; }
    public int AssetId { get; set; }
}
