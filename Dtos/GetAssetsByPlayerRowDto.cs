namespace BattleGame.Api.Dtos;

public record GetAssetsByPlayerRowDto(
    int No,
    string PlayerName,
    int Level,
    int Age,
    string AssetName
);
