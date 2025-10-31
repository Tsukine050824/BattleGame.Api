namespace BattleGame.Api.Models;

public class Player
{
    public int Id { get; set; }
    public string PlayerName { get; set; } = default!;  // username trong game
    public string FullName { get; set; } = default!;
    public int Age { get; set; }
    public int CurrentLevel { get; set; }

    public ICollection<PlayerAsset> PlayerAssets { get; set; } = new List<PlayerAsset>();
}
