namespace BattleGame.Api.Models;

public sealed class RegisterPlayerRequest
{
    public string PlayerName { get; set; } = default!;
    public string? FullName { get; set; }
    public int? Age { get; set; }
    public int CurrentLevel { get; set; } = 1;
}

public sealed class CreateAssetRequest
{
    public string AssetName { get; set; } = default!;
    public string? AssetType { get; set; }
}

public sealed class PlayerAssetRow
{
    public int No { get; set; }
    public string PlayerName { get; set; } = default!;
    public int Level { get; set; }
    public int? Age { get; set; }
    public string AssetName { get; set; } = default!;
}
