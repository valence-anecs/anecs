#!/usr/bin/env bash
set -euo pipefail

dotnet restore Anecs.slnx
dotnet build Anecs.slnx --no-restore --configuration Release
dotnet test Anecs.slnx --no-build --configuration Release
PYTHONPATH=python/src python -m compileall -q python/src python/tests
PYTHONPATH=python/src python -m unittest discover -s python/tests -v
python scripts/validate_fixtures.py
