using BattleGame.Api.Repositories;

namespace BattleGame.Api.Endpoints;

public static class ReportEndpoints
{
    public static IEndpointRouteBuilder MapReportEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/getassetsbyplayer", async (ReportRepository repo) =>
        {
            var rows = await repo.GetAssetsByPlayerAsync();
            return Results.Ok(rows);
        })
        .WithName("getassetsbyplayer")
        .WithSummary("Report: assets of players")
        .WithOpenApi();

        return app;
    }
}
