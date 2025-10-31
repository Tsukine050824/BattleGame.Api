using Dapper;
using BattleGame.Api.Models;
using BattleGame.Api.Data;
using MySqlConnector; // <- để bắt lỗi MySQL (mã 1062, 1452)

namespace BattleGame.Api.Repositories;

public sealed class AssetRepository
{
    private readonly MySqlDb _db;
    public AssetRepository(MySqlDb db) => _db = db;

    public async Task<int> CreateAsync(CreateAssetRequest req)
    {
        const string sql = @"
INSERT INTO Asset (AssetName, AssetType)
VALUES (@AssetName, @AssetType);
SELECT LAST_INSERT_ID();";
        using var conn = _db.CreateConnection();
        return await conn.ExecuteScalarAsync<int>(sql, req);
    }

    // NEW: gán Asset cho Player
    public async Task<(bool inserted, string? note)> AssignAsync(int playerId, int assetId)
    {
        // Bảng PlayerAsset đã có UNIQUE (PlayerId, AssetId)
        const string sql = @"INSERT INTO PlayerAsset (PlayerId, AssetId) VALUES (@PlayerId, @AssetId);";
        using var conn = _db.CreateConnection();
        try
        {
            var rows = await conn.ExecuteAsync(sql, new { PlayerId = playerId, AssetId = assetId });
            return (rows > 0, null);
        }
        catch (MySqlException ex) when (ex.Number == 1062) // duplicate key
        {
            return (false, "This asset has already been assigned to this player.");
        }
        catch (MySqlException ex) when (ex.Number == 1452) // foreign key fail
        {
            return (false, "Player or Asset not found (FK constraint failed).");
        }
    }
}
