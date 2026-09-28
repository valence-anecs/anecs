using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Anecs.Contracts;

namespace Anecs.Core;

public sealed class SnapshotSealService
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false
    };

    public SealedSnapshot Seal(
        IEnumerable<StateEstimate> state,
        string ontologyVersion,
        string policyVersion,
        DateTimeOffset sealedAt)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(ontologyVersion);
        ArgumentException.ThrowIfNullOrWhiteSpace(policyVersion);

        var orderedState = state
            .OrderBy(item => item.SubjectId, StringComparer.Ordinal)
            .ThenBy(item => item.Predicate, StringComparer.Ordinal)
            .ThenBy(item => item.EstimateId, StringComparer.Ordinal)
            .Select(Normalize)
            .ToArray();

        var envelope = new SnapshotEnvelope(
            ontologyVersion,
            policyVersion,
            sealedAt.ToUniversalTime(),
            orderedState);

        var canonicalJson = JsonSerializer.Serialize(envelope, JsonOptions);
        var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(canonicalJson));
        var hash = Convert.ToHexStringLower(hashBytes);

        return new SealedSnapshot(
            $"snapshot:sha256:{hash}",
            hash,
            ontologyVersion,
            policyVersion,
            sealedAt.ToUniversalTime(),
            orderedState);
    }

    private static StateEstimate Normalize(StateEstimate estimate) => estimate with
    {
        EvidenceRelationIds = estimate.EvidenceRelationIds
            .OrderBy(id => id, StringComparer.Ordinal)
            .ToArray(),
        ValidFrom = estimate.ValidFrom?.ToUniversalTime(),
        ValidTo = estimate.ValidTo?.ToUniversalTime()
    };

    private sealed record SnapshotEnvelope(
        string OntologyVersion,
        string PolicyVersion,
        DateTimeOffset SealedAt,
        IReadOnlyList<StateEstimate> State);
}
