CREATE SCHEMA IF NOT EXISTS anecs;

CREATE TABLE IF NOT EXISTS anecs.schema_version
(
    version_id text PRIMARY KEY,
    applied_at timestamptz NOT NULL DEFAULT now()
);

INSERT INTO anecs.schema_version (version_id)
VALUES ('bootstrap-0001')
ON CONFLICT DO NOTHING;
