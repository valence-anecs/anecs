#!/usr/bin/env python3
"""Dependency-free structural checks for bootstrap conformance fixtures."""

from __future__ import annotations

import json
from pathlib import Path


ROOT = Path(__file__).resolve().parents[1]
FIXTURES = ROOT / "fixtures" / "conformance"
VALID_STATUSES = {"Known", "Unknown", "Conflicting", "Unsupported"}
REQUIRED = {"estimateId", "subjectId", "predicate", "status", "evidenceRelationIds"}


def validate_state_estimate(path: Path) -> list[str]:
    value = json.loads(path.read_text(encoding="utf-8"))
    errors: list[str] = []
    missing = sorted(REQUIRED - value.keys())
    if missing:
        errors.append(f"missing fields: {', '.join(missing)}")
    if value.get("status") not in VALID_STATUSES:
        errors.append(f"invalid status: {value.get('status')!r}")
    evidence = value.get("evidenceRelationIds")
    if not isinstance(evidence, list) or len(evidence) != len(set(evidence)):
        errors.append("evidenceRelationIds must be a unique array")
    if value.get("status") == "Known" and value.get("canonicalValueJson") is None:
        errors.append("Known state requires canonicalValueJson")
    if value.get("status") != "Known" and value.get("canonicalValueJson") is not None:
        errors.append("non-Known state must not assert canonicalValueJson")
    return errors


def main() -> int:
    failures: list[str] = []
    paths = sorted(FIXTURES.glob("state-estimate.*.json"))
    if not paths:
        failures.append("no state-estimate fixtures found")
    for path in paths:
        failures.extend(f"{path.relative_to(ROOT)}: {error}" for error in validate_state_estimate(path))
    if failures:
        print("\n".join(failures))
        return 1
    print(f"validated {len(paths)} conformance fixture(s)")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
