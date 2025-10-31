using BattleGame.Api.Data;
using BattleGame.Api.Dtos;
using BattleGame.Api.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// EF Core 9 + Pomelo MySQL
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    var cs = builder.Configuration.GetConnectionString("DefaultConnection");
    // Pomelo (xóa đi): opt.UseMySql(cs, ServerVersion.AutoDetect(cs));
    // Oracle provider:
    opt.UseMySQL(cs);
});

// (Tuỳ chọn) CORS nếu front-end gọi khác origin
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("any", p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

app.UseCors("any");

// ---- API theo đề bài ----

// 1) registerplayer
app.MapPost("/api/registerplayer", async (RegisterPlayerDto dto, AppDbContext db) =>
{
    var exists = await db.Players.AnyAsync(p => p.PlayerName == dto.PlayerName);
    if (exists) return Results.Conflict($"PlayerName '{dto.PlayerName}' đã tồn tại.");

    var p = new Player
    {
        PlayerName = dto.PlayerName.Trim(),
        FullName = dto.FullName.Trim(),
        Age = dto.Age,
        CurrentLevel = dto.CurrentLevel
    };
    db.Players.Add(p);
    await db.SaveChangesAsync();
    return Results.Created($"/api/players/{p.Id}", new { p.Id, p.PlayerName, p.FullName, p.Age, p.CurrentLevel });
});

// 2) createasset
app.MapPost("/api/createasset", async (CreateAssetDto dto, AppDbContext db) =>
{
    var a = new Asset { AssetName = dto.AssetName.Trim(), Description = dto.Description };
    db.Assets.Add(a);
    await db.SaveChangesAsync();
    return Results.Created($"/api/assets/{a.Id}", a);
});

// (Phụ) assign asset cho player
app.MapPost("/api/assignasset/{playerId:int}/{assetId:int}", async (int playerId, int assetId, AppDbContext db) =>
{
    var ok = await db.Players.AnyAsync(p => p.Id == playerId) && await db.Assets.AnyAsync(a => a.Id == assetId);
    if (!ok) return Results.NotFound("Player hoặc Asset không tồn tại.");

    var exists = await db.PlayerAssets.FindAsync(playerId, assetId);
    if (exists != null) return Results.Conflict("Player đã có Asset này.");

    db.PlayerAssets.Add(new PlayerAsset { PlayerId = playerId, AssetId = assetId });
    await db.SaveChangesAsync();
    return Results.Ok(new { Message = "Assigned." });
});

// 3) getassetsbyplayer
app.MapGet("/api/getassetsbyplayer", async (AppDbContext db) =>
{
    var rows = await db.PlayerAssets
        .AsNoTracking()
        .OrderBy(pa => pa.PlayerId).ThenBy(pa => pa.AssetId)
        .Select(pa => new
        {
            pa.Player.PlayerName,
            Level = pa.Player.CurrentLevel,
            Age = pa.Player.Age,
            AssetName = pa.Asset.AssetName
        })
        .ToListAsync();

    var result = rows.Select((r, i) => new GetAssetsByPlayerRowDto(i + 1, r.PlayerName, r.Level, r.Age, r.AssetName));
    return Results.Ok(result);
});

app.Run();
