namespace BattleGame.Api.Models;

public sealed class Player
{
    public int Id { get; set; }
    public string PlayerName { get; set; } = default!;
    public string? FullName { get; set; }
    public int? Age { get; set; }
    public int CurrentLevel { get; set; } = 1;
    public DateTime CreatedAt { get; set; }
}
