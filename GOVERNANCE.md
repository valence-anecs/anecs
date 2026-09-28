# Governance

ANECS is currently in a founder-led validation phase. Governance optimizes for falsifiability, deterministic behavior and a durable evidence trail rather than feature throughput.

## Roles

### Maintainer

Maintainers approve repository policy, canonical contracts, releases and changes to decision gates.

### Contributor

Contributors may propose changes through issues and pull requests. Contribution does not imply authority to alter accepted state, validation outcomes or governance records outside review.

### Evaluator

Evaluators label experiment outcomes using a versioned rubric. Evaluator disagreement and adjudication are retained as data.

## Decision classes

| Class | Examples | Required record |
|---|---|---|
| Architectural | Repository boundaries, persistence, protocol | ADR |
| Semantic | Primitive or invariant change | Competency question and fixtures |
| Experimental | Baseline, metric or acceptance threshold | Experiment protocol |
| Governance | Roles, disclosure, release policy | Governance pull request |

## Merge policy

Changes to `main` require a pull request and passing CI. During the solo phase, automated gates are mandatory and human review may be self-review. Once a second maintainer joins, canonical contract and governance changes require at least one independent approval.

## Research integrity

- Rejected hypotheses remain visible.
- Metrics and acceptance thresholds must be defined before outcome inspection.
- Failed and negative fixtures are first-class artifacts.
- Experiment inputs are frozen and content-addressed.
- A result is not presented as replicated unless independently rerun from its replay package.
