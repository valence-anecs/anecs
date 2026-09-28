using Anecs.Contracts;

namespace Anecs.Core;

public sealed class P0ActionSelector
{
    public const string PolicyVersion = "p0.1";

    public CoordinationAction Select(IEnumerable<CoordinationActionCandidate> candidates)
    {
        var valid = candidates
            .Where(candidate => candidate.IsAuthorized)
            .Select(Validate)
            .OrderByDescending(candidate => candidate.IsHardBlocker)
            .ThenByDescending(candidate => candidate.DecisionImpactRank)
            .ThenBy(candidate => candidate.EstimatedCost)
            .ThenBy(candidate => candidate.ExpectedLatency)
            .ThenBy(candidate => candidate.ActionType, StringComparer.Ordinal)
            .ThenBy(candidate => candidate.TargetEntityId, StringComparer.Ordinal)
            .ThenBy(candidate => candidate.UnknownId, StringComparer.Ordinal)
            .ToArray();

        if (valid.Length == 0)
        {
            throw new InvalidOperationException("P0_NO_AUTHORIZED_ACTION");
        }

        var selected = valid[0];
        var rationale = selected.IsHardBlocker
            ? "P0_HARD_BLOCKER_LOWEST_COST"
            : "P0_HIGHEST_IMPACT_LOWEST_COST";

        return new CoordinationAction(
            selected.ActionType,
            selected.TargetEntityId,
            selected.UnknownId,
            PolicyVersion,
            rationale);
    }

    private static CoordinationActionCandidate Validate(CoordinationActionCandidate candidate)
    {
        if (candidate.DecisionImpactRank < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(candidate), "Decision impact rank cannot be negative.");
        }

        if (candidate.EstimatedCost < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(candidate), "Estimated cost cannot be negative.");
        }

        if (candidate.ExpectedLatency < TimeSpan.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(candidate), "Expected latency cannot be negative.");
        }

        return candidate;
    }
}
