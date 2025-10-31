using Dapper;
using BattleGame.Api.Models;
using BattleGame.Api.Data;

namespace BattleGame.Api.Repositories;

public sealed class PlayerRepository
{
    private readonly MySqlDb _db;
    public PlayerRepository(MySqlDb db) => _db = db;

    public async Task<int> RegisterAsync(RegisterPlayerRequest req)
    {
        const string sql = @"
INSERT INTO Player (PlayerName, FullName, Age, CurrentLevel)
VALUES (@PlayerName, @FullName, @Age, @CurrentLevel);
SELECT LAST_INSERT_ID();";
        using var conn = _db.CreateConnection();
        var id = await conn.ExecuteScalarAsync<int>(sql, req);
        return id;
    }
}
