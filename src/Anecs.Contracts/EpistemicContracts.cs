namespace Anecs.Contracts;

public enum EpistemicStatus
{
    Known,
    Unknown,
    Conflicting,
    Unsupported
}

public sealed record Observation(
    string ObservationId,
    string SourceSubjectId,
    string Predicate,
    string CanonicalValueJson,
    DateTimeOffset AcquiredAt,
    DateTimeOffset? ValidFrom,
    DateTimeOffset? ValidTo);

public sealed record Claim(
    string ClaimId,
    string SubjectId,
    string Predicate,
    string CanonicalValueJson,
    IReadOnlyList<string> ObservationIds);

public enum EvidenceRelationKind
{
    Supports,
    Contradicts,
    Qualifies
}

public sealed record EvidenceRelation(
    string RelationId,
    string ClaimId,
    string ObservationId,
    EvidenceRelationKind Kind,
    string RationaleCode);

public sealed record StateEstimate(
    string EstimateId,
    string SubjectId,
    string Predicate,
    EpistemicStatus Status,
    string? CanonicalValueJson,
    IReadOnlyList<string> EvidenceRelationIds,
    DateTimeOffset? ValidFrom,
    DateTimeOffset? ValidTo);

public sealed record SealedSnapshot(
    string SnapshotId,
    string SnapshotHash,
    string OntologyVersion,
    string PolicyVersion,
    DateTimeOffset SealedAt,
    IReadOnlyList<StateEstimate> State);
