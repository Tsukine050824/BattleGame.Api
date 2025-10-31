using System.Data;
using MySqlConnector;
using Microsoft.Extensions.Options;

namespace BattleGame.Api.Data;

public sealed class MySqlDb
{
    private readonly string _connStr;
    public MySqlDb(IConfiguration config)
    {
        _connStr = config.GetConnectionString("MySql") 
                   ?? throw new InvalidOperationException("Missing ConnectionStrings:MySql");
    }

    public IDbConnection CreateConnection() => new MySqlConnection(_connStr);
}
