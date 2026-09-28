# ADR-0001: Validation-first monorepo

- Status: Accepted
- Date: 2026-09-28

## Context

ANECS combines ontology, identity resolution, evidence state, temporal replay, policy selection and economic evaluation. Splitting these concerns into independent repositories before their contracts stabilize would make changes difficult to evaluate atomically.

## Decision

The bootstrap implementation uses one monorepo containing contracts, deterministic core logic, research utilities, conformance fixtures, tests and experiment specifications.

Every behavior-changing pull request must identify:

1. the affected invariant or competency question;
2. the hypothesis or baseline it influences;
3. the fixture or measurement that can falsify it;
4. the expected replay behavior.

## Consequences

- Schema, implementation and fixture changes can be reviewed together.
- CI can prevent semantic drift between .NET and Python boundaries.
- Independent packages may be extracted only after stable ownership and versioning boundaries are observed.
