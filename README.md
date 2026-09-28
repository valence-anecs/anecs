# Valence ANECS

**Agent-Native Economic Coordination Space** is a validation-first reference environment for turning heterogeneous evidence into identity-safe, temporal economic state and selecting the next information action that can change a coordination decision.

ANECS does not begin with the claim that another supplier marketplace is needed. It tests a narrower proposition:

> Can provenance-aware, decision-sensitive coordination reduce search, interaction and waiting cost while preserving decision quality in complex B2B procurement?

## Repository status

This repository is the bootstrap reference implementation for the ANECS validation program. It is deliberately not a production marketplace.

The first executable vertical slice is:

```text
Observation → Claim → EvidenceRelation → StateEstimate
            → SealedSnapshot → DecisionSensitiveUnknown → P0 Action
```

The canonical decision path is deterministic. LLMs and extractors may propose observations or claims, but they are not authoritative state writers.

## Workspaces

| Path | Responsibility |
|---|---|
| `src/Anecs.Contracts` | Canonical contracts and lifecycle types |
| `src/Anecs.Core` | Deterministic snapshot and policy logic |
| `src/Anecs.Api` | Minimal reference API |
| `src/Anecs.Cli` | Automation and replay entry point |
| `python/` | Acquisition and research utilities |
| `schemas/` | Machine-readable conformance contracts |
| `fixtures/` | Positive, negative and replay fixtures |
| `tests/` | Unit, conformance and deterministic replay tests |
| `docs/` | Architecture, ADRs and validation program |
| `backlog/` | Seed work items before GitHub issue creation |

## Quick start

The recommended path is VS Code **Dev Containers** or GitHub Codespaces. The container pins .NET 10, Python 3.14 and `uv`.

```bash
cp .env.example .env
# Set ANECS_POSTGRES_PASSWORD in .env before starting PostgreSQL.
docker compose up -d postgres
./scripts/check.sh
dotnet run --project src/Anecs.Api
```

On Windows PowerShell:

```powershell
Copy-Item .env.example .env
# Set ANECS_POSTGRES_PASSWORD in .env before starting PostgreSQL.
docker compose up -d postgres
./scripts/check.ps1
dotnet run --project src/Anecs.Api
```

Health check: `GET http://localhost:5080/health`

## Validation conditions

| Code | Condition | Isolates |
|---|---|---|
| B0 | Manual expert baseline | Current human process |
| B1 | Catalog and keyword baseline | Conventional discovery |
| T1 | Semantic match | Representation contribution |
| T2 | Semantic plus evidence | Identity, provenance, time and conflict |
| T3 | Full ANECS P0 | DSU, readiness and next action |
| T4 | Expert-selected DSU | P0 policy versus expert heuristic |

See [`docs/research/validation-matrix.md`](docs/research/validation-matrix.md).

## Non-goals for bootstrap

- Autonomous purchasing or contractual commitment
- Universal supplier scoring
- LLM authority over canonical state
- Production-scale distributed infrastructure
- Token, settlement or blockchain requirements
- Replacing price as an economic coordination signal

## Contribution

All changes must preserve deterministic build, conformance and replay. Read [`CONTRIBUTING.md`](CONTRIBUTING.md) and [`GOVERNANCE.md`](GOVERNANCE.md) before opening a pull request.

## License

No open-source license has been granted yet. The repository is source-available for project validation while the code/specification licensing boundary is decided.
