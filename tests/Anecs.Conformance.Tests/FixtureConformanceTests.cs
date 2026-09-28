using System.Text.Json;
using System.Text.Json.Serialization;
using Anecs.Contracts;

namespace Anecs.Conformance.Tests;

public sealed class FixtureConformanceTests
{
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };

    [Fact]
    public void StateEstimateFixture_IsReadableAndPreservesConflict()
    {
        var path = Path.Combine(AppContext.BaseDirectory, "fixtures", "state-estimate.conflicting.json");
        var estimate = JsonSerializer.Deserialize<StateEstimate>(File.ReadAllText(path), Options);

        Assert.NotNull(estimate);
        Assert.Equal(EpistemicStatus.Conflicting, estimate.Status);
        Assert.Equal(2, estimate.EvidenceRelationIds.Count);
        Assert.Null(estimate.CanonicalValueJson);
    }
}
