namespace BattleGame.Api.Dtos;

public record RegisterPlayerDto(string PlayerName, string FullName, int Age, int CurrentLevel);
