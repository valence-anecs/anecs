#!/usr/bin/env python3
"""Create seed labels and issues. Dry-run is the default and has no side effects."""

from __future__ import annotations

import argparse
import json
import subprocess
from pathlib import Path


ROOT = Path(__file__).resolve().parents[1]


def gh(*args: str, capture: bool = False) -> str:
    completed = subprocess.run(
        ["gh", *args],
        check=True,
        text=True,
        capture_output=capture,
    )
    return completed.stdout.strip() if capture else ""


def load(name: str) -> list[dict[str, object]]:
    return json.loads((ROOT / "backlog" / name).read_text(encoding="utf-8"))


def existing_titles(repo: str) -> set[str]:
    payload = gh(
        "issue",
        "list",
        "--repo",
        repo,
        "--state",
        "all",
        "--limit",
        "500",
        "--json",
        "title",
        capture=True,
    )
    return {item["title"] for item in json.loads(payload)}


def apply(repo: str) -> None:
    for label in load("labels.json"):
        gh(
            "label",
            "create",
            str(label["name"]),
            "--repo",
            repo,
            "--color",
            str(label["color"]),
            "--description",
            str(label["description"]),
            "--force",
        )

    titles = existing_titles(repo)
    for issue in load("issues.json"):
        title = f'{issue["key"]}: {issue["title"]}'
        if title in titles:
            print(f"skip existing: {title}")
            continue
        args = [
            "issue",
            "create",
            "--repo",
            repo,
            "--title",
            title,
            "--body",
            str(issue["body"]),
        ]
        for label in issue["labels"]:
            args.extend(["--label", str(label)])
        gh(*args)


def main() -> int:
    parser = argparse.ArgumentParser()
    parser.add_argument("--repo", default="valence-anecs/anecs")
    parser.add_argument("--apply", action="store_true")
    args = parser.parse_args()

    if not args.apply:
        print(json.dumps({"repo": args.repo, "labels": load("labels.json"), "issues": load("issues.json")}, indent=2))
        print("\nDry-run only. Re-run with --apply after reviewing the output.")
        return 0

    apply(args.repo)
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
