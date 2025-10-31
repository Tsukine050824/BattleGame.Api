using BattleGame.Api.Models;
using BattleGame.Api.Repositories;

namespace BattleGame.Api.Endpoints;

public static class PlayerEndpoints
{
    public static IEndpointRouteBuilder MapPlayerEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/registerplayer", async (RegisterPlayerRequest req, PlayerRepository repo) =>
        {
            if (string.IsNullOrWhiteSpace(req.PlayerName))
                return Results.BadRequest("PlayerName is required");
            var id = await repo.RegisterAsync(req);
            return Results.Created($"/players/{id}", new { id });
        })
        .WithName("registerplayer")
        .WithSummary("Register a new player")
        .WithOpenApi();

        return app;
    }
}
