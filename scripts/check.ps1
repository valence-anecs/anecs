$ErrorActionPreference = "Stop"

dotnet restore Anecs.slnx
dotnet build Anecs.slnx --no-restore --configuration Release
dotnet test Anecs.slnx --no-build --configuration Release
$env:PYTHONPATH = "python/src"
python -m compileall -q python/src python/tests
python -m unittest discover -s python/tests -v
python scripts/validate_fixtures.py
