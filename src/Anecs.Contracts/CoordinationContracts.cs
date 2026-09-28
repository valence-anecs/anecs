namespace Anecs.Contracts;

public sealed record DecisionSensitiveUnknown(
    string UnknownId,
    string SubjectId,
    string Predicate,
    bool IsHardBlocker,
    int DecisionImpactRank,
    string RationaleCode);

public sealed record CoordinationActionCandidate(
    string ActionType,
    string TargetEntityId,
    string UnknownId,
    bool IsHardBlocker,
    int DecisionImpactRank,
    decimal EstimatedCost,
    TimeSpan ExpectedLatency,
    bool IsAuthorized);

public sealed record CoordinationAction(
    string ActionType,
    string TargetEntityId,
    string UnknownId,
    string PolicyVersion,
    string RationaleCode);
