using Anecs.Contracts;
using Anecs.Core;

namespace Anecs.Core.Tests;

public sealed class P0ActionSelectorTests
{
    [Fact]
    public void Select_PrefersHardBlockerBeforeLowerPriorityUnknown()
    {
        var selector = new P0ActionSelector();
        var candidates = new[]
        {
            Candidate("RequestQuote", "supplier:a", "unknown:quote", false, 9, 1m, 1),
            Candidate("RequestCertification", "supplier:b", "unknown:cert", true, 4, 5m, 10)
        };

        var selected = selector.Select(candidates);

        Assert.Equal("RequestCertification", selected.ActionType);
        Assert.Equal("unknown:cert", selected.UnknownId);
        Assert.Equal("P0_HARD_BLOCKER_LOWEST_COST", selected.RationaleCode);
    }

    [Fact]
    public void Select_IsDeterministicWhenCandidatesArriveInDifferentOrder()
    {
        var selector = new P0ActionSelector();
        var first = Candidate("RequestAvailability", "supplier:a", "unknown:a", true, 5, 2m, 2);
        var second = Candidate("RequestCapabilityEvidence", "supplier:b", "unknown:b", true, 5, 2m, 2);

        var forward = selector.Select(new[] { first, second });
        var reverse = selector.Select(new[] { second, first });

        Assert.Equal(forward, reverse);
    }

    [Fact]
    public void Select_RejectsWhenNoActionIsAuthorized()
    {
        var selector = new P0ActionSelector();
        var candidate = Candidate("RequestQuote", "supplier:a", "unknown:quote", false, 3, 1m, 1) with
        {
            IsAuthorized = false
        };

        var exception = Assert.Throws<InvalidOperationException>(() => selector.Select(new[] { candidate }));

        Assert.Equal("P0_NO_AUTHORIZED_ACTION", exception.Message);
    }

    private static CoordinationActionCandidate Candidate(
        string actionType,
        string target,
        string unknown,
        bool hardBlocker,
        int impact,
        decimal cost,
        int latencyMinutes) => new(
            actionType,
            target,
            unknown,
            hardBlocker,
            impact,
            cost,
            TimeSpan.FromMinutes(latencyMinutes),
            true);
}
