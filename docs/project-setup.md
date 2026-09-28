# GitHub project setup

Create one organization-level project named **ANECS Validation Program**.

## Fields

| Field | Type | Values |
|---|---|---|
| Status | Single select | Backlog, Ready, In Progress, Review, Validated, Rejected, Blocked |
| Phase | Single select | Bootstrap, Baseline, T1, T2, T3, Transfer |
| Workstream | Single select | Contracts, Identity, Evidence, Time, Policy, Data, Evaluation, Infrastructure |
| Hypothesis | Single select | H1, H2, H3, H4, H5, H6, H7, H8, N/A |
| Evidence status | Single select | Proposed, Implemented, Measured, Replicated |
| Priority | Single select | P0, P1, P2, P3 |
| Decision gate | Single select | Technical, Epistemic, Economic, GO/NO-GO |
| Risk | Single select | Low, Medium, High, Critical |

## Views

1. **Roadmap** grouped by Phase.
2. **Active work** filtered to Ready, In Progress and Review.
3. **Hypotheses** grouped by Hypothesis and Evidence status.
4. **Decision gates** grouped by Decision gate.
5. **Rejected learning** filtered to Rejected; these items are never hidden.

Use GitHub sub-issues for Epic → Hypothesis/Feature → Experiment/Task relationships. Do not duplicate the hierarchy in Markdown task lists after the relationship exists.
