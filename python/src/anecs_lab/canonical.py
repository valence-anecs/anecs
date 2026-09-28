"""Deterministic helpers shared by acquisition and experiment tooling."""

from __future__ import annotations

import hashlib
import json
from typing import Any


def canonical_json(value: Any) -> str:
    """Serialize JSON-compatible data with stable ordering and no insignificant whitespace."""

    return json.dumps(value, ensure_ascii=False, separators=(",", ":"), sort_keys=True)


def sha256_id(prefix: str, value: Any) -> str:
    """Return a content-addressed identifier for a JSON-compatible value."""

    payload = canonical_json(value).encode("utf-8")
    digest = hashlib.sha256(payload).hexdigest()
    return f"{prefix}:sha256:{digest}"
