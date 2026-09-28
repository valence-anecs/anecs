using Anecs.Contracts;
using Anecs.Core;

namespace Anecs.Replay.Tests;

public sealed class SnapshotReplayTests
{
    [Fact]
    public void Seal_ProducesSameHashForEquivalentStateInDifferentOrder()
    {
        var service = new SnapshotSealService();
        var sealedAt = DateTimeOffset.Parse("2026-09-28T12:00:00Z");
        var stateA = Estimate("estimate:a", "supplier:a", "capability:cnc", "relation:2", "relation:1");
        var stateB = Estimate("estimate:b", "supplier:b", "availability:2026-q4", "relation:3");

        var forward = service.Seal(new[] { stateA, stateB }, "ontology:0.1", "policy:p0.1", sealedAt);
        var reverse = service.Seal(new[] { stateB, stateA }, "ontology:0.1", "policy:p0.1", sealedAt);

        Assert.Equal(forward.SnapshotHash, reverse.SnapshotHash);
        Assert.Equal(forward.SnapshotId, reverse.SnapshotId);
    }

    private static StateEstimate Estimate(
        string id,
        string subject,
        string predicate,
        params string[] evidence) => new(
            id,
            subject,
            predicate,
            EpistemicStatus.Known,
            "true",
            evidence,
            DateTimeOffset.Parse("2026-01-01T00:00:00Z"),
            null);
}
