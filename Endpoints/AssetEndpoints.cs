using BattleGame.Api.Models;
using BattleGame.Api.Repositories;

namespace BattleGame.Api.Endpoints;

public static class AssetEndpoints
{
    public static IEndpointRouteBuilder MapAssetEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/createasset", async (CreateAssetRequest req, AssetRepository repo) =>
        {
            if (string.IsNullOrWhiteSpace(req.AssetName))
                return Results.BadRequest("AssetName is required");
            var id = await repo.CreateAsync(req);
            return Results.Created($"/assets/{id}", new { id });
        })
        .WithName("createasset")
        .WithSummary("Create a new asset")
        .WithOpenApi();

         app.MapPost("/assignasset", async (AssignAssetRequest req, AssetRepository repo) =>
        {
            if (req.PlayerId <= 0 || req.AssetId <= 0)
                return Results.BadRequest("PlayerId and AssetId must be positive integers.");

            var (inserted, note) = await repo.AssignAsync(req.PlayerId, req.AssetId);
            return inserted
                ? Results.Ok(new { assigned = true, playerId = req.PlayerId, assetId = req.AssetId })
                : Results.BadRequest(new { assigned = false, note });
        })
        .WithName("assignasset")
        .WithSummary("Assign an asset to a player")
        .WithOpenApi();

        return app;
    }
}
