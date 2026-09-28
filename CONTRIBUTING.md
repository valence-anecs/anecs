# Contributing

## Before implementation

Open or select an issue that states the affected hypothesis, invariant, baseline or decision gate. For architecture changes, add an ADR under `docs/adr/`.

## Local checks

Linux/dev container:

```bash
./scripts/check.sh
```

Windows PowerShell:

```powershell
./scripts/check.ps1
```

## Pull-request requirements

- Describe the problem and the decision it changes.
- Add or update positive and negative fixtures.
- Preserve deterministic replay or explicitly version the break.
- Do not let extraction or LLM output bypass canonical validation.
- Record new dependencies and why the standard library is insufficient.
- Link the relevant hypothesis or experiment issue.

## Commit guidance

Use imperative, scoped messages such as:

```text
contracts: add acquisition time to Observation
policy: reject unauthorized P0 candidates
fixtures: add conflicting certificate evidence
```
