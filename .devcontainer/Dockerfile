FROM ghcr.io/astral-sh/uv:0.12.18 AS uv
FROM mcr.microsoft.com/devcontainers/dotnet:1-10.0-noble

USER root
COPY --from=uv /uv /uvx /usr/local/bin/
RUN apt-get update \
    && apt-get install -y --no-install-recommends jq postgresql-client \
    && rm -rf /var/lib/apt/lists/*

USER vscode
