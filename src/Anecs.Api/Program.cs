using Anecs.Contracts;
using Anecs.Core;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<SnapshotSealService>();
builder.Services.AddSingleton<P0ActionSelector>();

var app = builder.Build();

app.MapGet("/health", () => Results.Ok(new
{
    service = "anecs-api",
    status = "ok",
    policy = P0ActionSelector.PolicyVersion
}));

app.MapPost("/v0/snapshots/seal", (
    SnapshotSealRequest request,
    SnapshotSealService service) =>
{
    var snapshot = service.Seal(
        request.State,
        request.OntologyVersion,
        request.PolicyVersion,
        request.SealedAt);

    return Results.Ok(snapshot);
});

app.MapPost("/v0/policies/p0/select", (
    IReadOnlyList<CoordinationActionCandidate> candidates,
    P0ActionSelector selector) => Results.Ok(selector.Select(candidates)));

app.Run();

public sealed record SnapshotSealRequest(
    string OntologyVersion,
    string PolicyVersion,
    DateTimeOffset SealedAt,
    IReadOnlyList<StateEstimate> State);
