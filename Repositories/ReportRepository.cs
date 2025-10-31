using Dapper;
using BattleGame.Api.Models;
using BattleGame.Api.Data;

namespace BattleGame.Api.Repositories;

public sealed class ReportRepository
{
    private readonly MySqlDb _db;
    public ReportRepository(MySqlDb db) => _db = db;

    // getassetsbyplayer: liệt kê mọi Player-Asset (giống table mẫu)
    public async Task<IEnumerable<PlayerAssetRow>> GetAssetsByPlayerAsync()
    {
        const string sql = @"
SELECT
    ROW_NUMBER() OVER (ORDER BY p.Id, a.Id) AS No,
    p.PlayerName AS PlayerName,
    p.CurrentLevel AS Level,
    p.Age AS Age,
    a.AssetName AS AssetName
FROM PlayerAsset pa
JOIN Player p ON p.Id = pa.PlayerId
JOIN Asset a ON a.Id = pa.AssetId
ORDER BY p.Id, a.Id;";
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<PlayerAssetRow>(sql);
    }
}
